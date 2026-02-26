#!/usr/bin/env python3
"""
OHS Program — Excel → PostgreSQL Veri İçe Aktarma Scripti
==========================================================
Kaynak dosyalar:
  • Veri.xlsx                              → Personnels + Accidents tablolarına
  • İş_Kazaları_Genişletilmiş_Kodlama.xlsx → Kod → İsim sözlükleri
  • Veri Yevmiye.xlsx                      → AccidentStatistics tablosuna

Özellikler:
  • İdempotent: Tekrar çalıştırılabilir, var olan kayıtları atlar
  • Kuru çalıştırma: --dry-run argümanıyla gerçek insert yapmaz
  • İşletme: GLİ / ELİ / ÇLİ / TKİ olarak normalize edilir
  • Kodlama: Sanatı (TİS No) / Yer / Neden tam eşleşme, kısmi geri dönüş
  • Uzuv: 1-8 basit sisteme manuel haritalama uygulanır
"""

import sys
import uuid
import argparse
from datetime import datetime, date

import pandas as pd
import psycopg2

# ─────────────────────────────────────────────────────────────────────────────
# AYARLAR
# ─────────────────────────────────────────────────────────────────────────────

import os

# Excel dosyaları OHS_Program ana klasöründe (git'e gitmemesi için)
# Scripts -> ExcelDataImport -> OHS_project_api -> OHS_Program
SCRIPT_DIR   = os.path.dirname(os.path.abspath(__file__))           # Scripts
BASE_DIR     = os.path.dirname(SCRIPT_DIR)                          # ExcelDataImport
PROJECT_ROOT = os.path.dirname(BASE_DIR)                            # OHS_project_api  
OHS_ROOT     = os.path.dirname(PROJECT_ROOT)                        # OHS_Program

VERI_FILE    = os.path.join(OHS_ROOT, "Veri.xlsx")
YEVMIYE_FILE = os.path.join(OHS_ROOT, "Veri Yevmiye.xlsx")

DB = {
    "host":     "localhost",
    "port":     5432,
    "database": "OHSProgramApi",
    "user":     "postgres",
    "password": "123456",
}

# Veri Yevmiye.xlsx'teki Türkçe ay adları → 2 basamaklı sayı
MONTHS_TR = {
    "Ocak": "01", "Şubat": "02", "Mart": "03", "Nisan": "04",
    "Mayıs": "05", "Haziran": "06", "Temmuz": "07", "Ağustos": "08",
    "Eylül": "09", "Ekim": "10", "Kasım": "11", "Aralık": "12",
}

# Kazalanan Uzuv / Vücut Bölgesi Kodlamaları
# Eski sistem (1-8) : Veri.xlsx mevcut veriler için (geçici, sonra güncellenecek)
# Yeni sistem (10-99): İş Kazaları Kodlama.xlsx (resmi GLİ sistemi)
UZUV_MAP = {
    # ── Eski sistem (1-8) — Geçici ──
    1: "Baş / Yüz / Göz",
    2: "Boyun / Üst Gövde",
    3: "Kol / El / Parmak",
    4: "Alt Gövde / Bel / Sırt",
    5: "Bacak / Ayak",
    6: "Birden Fazla Bölge",
    7: "İç Organlar",
    8: "Diğer / Belirtilmemiş",
    # ── Baş Bölgesi (10–15) ──
    10: "BAŞ (Genel)",
    11: "Kafa / Kafatası",
    12: "Göz",
    13: "Kulak",
    14: "Burun",
    15: "Yüz / Çene / Alın",
    # ── Boyun (20) ──
    20: "BOYUN",
    # ── Üst Uzuv / Kol (30–37) ──
    30: "KOL (Genel)",
    31: "Omuz / Kürek Kemiği",
    32: "Üst Kol",
    33: "Dirsek",
    34: "Ön Kol",
    35: "El Bileği",
    36: "EL (Genel)",
    37: "Parmaklar (El)",
    # ── Alt Uzuv / Bacak (40–46) ──
    40: "BACAK (Genel)",
    41: "Kalça / Uyluk",
    42: "Diz",
    43: "Alt Bacak / Baldır",
    44: "Ayak Bileği",
    45: "AYAK (Genel)",
    46: "Parmaklar (Ayak)",
    # ── Gövde (50–55) ──
    50: "GÖVDE (Genel)",
    51: "Göğüs / Kaburgalar",
    52: "Sırt (Üst)",
    53: "Bel (Alt Sırt)",
    54: "Karın / Böbrek",
    55: "Pelvis / Leğen Kemiği",
    # ── Omurga / Diğer ──
    60: "OMURGA / DİSK",
    70: "İÇ ORGANLAR",
    80: "MUHTELİF (Çoklu Bölge)",
    90: "TÜM VÜCUT / SİSTEMİK",
    99: "BELİRTİLMEMİŞ / DİĞER",
}


# ─────────────────────────────────────────────────────────────────────────────
# KODLAMA SÖZLÜKLERI — İş Kazaları Kodlama.xlsx (GLİ Sistemi)
# ─────────────────────────────────────────────────────────────────────────────

# Unvan / Meslek Kodlamaları (GLİ Kod Sistemi)
# A — Yeraltı Çalışanları : 1–26
# B — Yerüstü Çalışanları : 51–105
TIS_MAP = {
    # ── A1 — Üretim / Kazı İşleri ──
     1: "MEKANİZE KAZI OPERATÖRÜ",
     2: "YÜRÜYEN TAHKİMAT SÜRÜCÜSÜ",
     3: "YER ALTI HAZIRLIK İŞÇİSİ",
     4: "YER ALTI HAZIRLIK İŞÇİSİ YEDEK",
     5: "YER ALTI HAZIRLIK USTASI",
     6: "YER ALTI MADEN İŞLETMESİ HAZIRLIK ELEMANI",
     7: "BEDEN İŞÇİSİ (YERALTI)",
     8: "DELİKÇİ",
     9: "ATEŞÇİ (DİNAMİTÇİ)",
    10: "ZEMİN DELGİ MAKİNESİ OPERATÖRÜ",
    11: "TAHKİMATÇI MADEN OCAĞI",
    # ── A2 — Nakliye / Taşıma ──
    12: "KONVEYÖR OPERATÖRÜ",
    13: "MONORAY OPERATÖRÜ",
    14: "DESANDRE VİNÇÇİSİ",
    15: "MALZEME TAKİP VE DAĞITIM İŞÇİSİ",
    # ── A3 — Bakım-Onarım / Teknik (Yeraltı) ──
    16: "TAMİR BAKIM USTA",
    17: "ELEKTRİKÇİ",
    18: "ELEKTRİK ELEKTRONİK TEKNİKERİ",
    19: "TULUMBACI MADEN OCAĞI",
    20: "KUMANDA MERKEZİ OPERATÖRÜ (KÖMÜR MADENİ)",
    21: "İŞLETME ELEKTRONİK BAKIMCISI",
    # ── A4 — Ölçüm / Gözetim / İSG (Yeraltı) ──
    22: "MADEN TEKNİKERİ",
    23: "ÖLÇÜ TESPİT ELEMANI",
    24: "ÖLÇÜ TESPİT SORUMLUSU",
    25: "NEZARETÇİ VE USTABAŞI",
    26: "YER ALTI İŞ SAĞLIĞI VE GÜVENLİĞİ DESTEK ELEMANI",
    # ── B1 — İş Makinesi / Araç Operatörleri ──
    51: "BULDOZER (DOZER) OPERATÖRÜ",
    52: "EKSKAVATÖR OPERATÖRÜ",
    53: "İŞ MAKİNELERİ OPERATÖRÜ",
    54: "GREYDER OPERATÖRÜ",
    55: "YÜKLEYİCİ OPERATÖRÜ",
    56: "FORKLİFT OPERATÖRÜ",
    57: "VİNÇ OPERATÖRÜ",
    58: "KAMYON ŞOFÖRÜ",
    59: "KAYA KAMYONU OPERATÖRÜ",
    60: "ŞOFÖR",
    # ── B2 — Tesis / Üretim Destek ──
    61: "TESİS İŞÇİSİ",
    62: "TESİS İŞÇİSİ 2",
    63: "MANEVRACI HARMANCI (MADEN)",
    64: "KANTAR GÖREVLİSİ",
    65: "NUMUNECİ (MADEN OCAĞI)",
    66: "BEDEN İŞÇİSİ",
    67: "DİĞER TAHMİL TAHLİYE VE İLGİLİ YÜK TAŞIMA İŞÇİLERİ",
    68: "AKARYAKIT İSTASYON SORUMLUSU",
    69: "AKARYAKIT VE OTOGAZ İKMAL VE DOLUM ELEMANI",
    # ── B3 — Bakım-Onarım / Atölye (Yerüstü) ──
    70: "İŞ MAKİNELERİ BAKIM VE ONARIMCISI",
    71: "İŞ MAKİNELERİ BAKIM VE ONARIMCISI 2",
    72: "MEKANİK BAKIM ONARIMCISI",
    73: "MEKANİK BAKIM ONARIMCISI 2",
    74: "KAYNAKÇI (OKSİJEN VE ELEKTRİK)",
    75: "SOĞUK DEMİRCİ",
    76: "TESVİYECİ",
    77: "TORNACI (TORNA TEZGAHI İŞÇİSİ)",
    78: "FREZECİ (FREZE TEZGAHI OPERATÖRÜ)",
    79: "DÖKÜM MODELCİSİ",
    80: "MARANGOZ",
    81: "RADYATÖR TAMİRCİSİ",
    82: "OTOMOTİV ELEKTRİKÇİSİ",
    83: "BOYACI",
    84: "İNŞAAT İŞÇİSİ",
    85: "İNŞAAT USTA",
    86: "ELEKTRİK TEKNİKERİ",
    87: "ELEKTRONİK TEKNİKERİ",
    # ── B4 — Ölçüm / Laboratuvar / Teknik (Yerüstü) ──
    88: "HARİTA TEKNİKERİ",
    89: "HARİTA TEKNİSYENİ",
    90: "TOPOĞRAF YARDIMCISI",
    91: "KİMYA TEKNİKERİ",
    92: "KİMYA TEKNİSYENİ",
    93: "İNŞAAT TEKNOLOJİSİ TEKNİKERİ",
    94: "MAKİNE TEKNİKERİ",
    95: "MAKİNE RESSAMI",
    # ── B5 — İdari / Destek Hizmetleri ──
     96: "İDARİ BÜRO GÖREVLİSİ 1",
     97: "İDARİ BÜRO GÖREVLİSİ 2",
     98: "BİLGİSAYAR İŞLETMENİ",
     99: "BİLGİSAYAR TEKNOLOJİSİ TEKNİKERİ",
    100: "TELEFON SANTRAL OPERATÖRÜ",
    101: "ÇEVRE SAĞLIĞI TEKNİKERİ / ÇEVRE TEKNİKERİ",
    102: "DİĞER SAĞLIK PERSONELİ",
    103: "AŞÇI",
    104: "ŞEF GARSON",
    105: "BAHÇIVAN",
}

# Kaza Yeri Kodlamaları
# A — Yeraltı (11–22)  |  B — Yerüstü (31–43)  |  C — Ulaşım (51–53)
YER_MAP = {
    # ── A — Yeraltı Kaza Yerleri ──
    11: "Ayak İçi / Mekanize Pano",
    12: "Hazırlık Galerisi / Malzeme Baca",
    13: "Kömür Baca / Kömür Yolu",
    14: "Ana İhraç Yolu / Galeri",
    15: "Bant Boyu / Konveyör Yolu",
    16: "Kuyu / Eğimli Kuyu",
    17: "Vinç Altı / Pompa İstasyonu",
    18: "Tertip Odası / Beke Yazıhane",
    19: "Kavşak / Sapak Noktası",
    20: "Havalandırma Yolu / Kaçamak",
    21: "Dolgu / Tavan Arası",
    22: "Diğer Yeraltı",
    # ── B — Yerüstü Kaza Yerleri ──
    31: "Tesisler (Kırıcı/Silo/Tikiner)",
    32: "Açık Ocak Sahası",
    33: "Atölyeler",
    34: "Pazarlama / Kantar Sahası",
    35: "Sosyal Tesisler / İdari Bina",
    36: "Servis Yolu / Ulaşım",
    37: "Laboratuvar",
    38: "Pasa Döküm / Stok Sahası",
    39: "Malzeme Deposu / Ambar",
    40: "Bant Hattı (Yerüstü)",
    41: "Park / Garaj Sahası",
    42: "Kuyu Ağzı / Kuyu Binası",
    43: "Diğer Yerüstü",
    # ── C — Ulaşım / Servis ──
    51: "Servis Aracı İçi",
    52: "İşyeri Giriş-Çıkış Yolu",
    53: "Diğer Ulaşım",
}

# Kaza Nedeni / Kaza Türü Kodlamaları (4 haneli string)
NEDEN_MAP = {
    # ── 01 — Göçük, Taş veya Kömür Düşmesi ──
    "0110": "Tavandan taş/kömür düşmesi",
    "0120": "Yan duvar (kanat) düşmesi",
    "0130": "Ayna göçüğü / Ayna düşmesi",
    "0140": "Bağ atımı sırasında malzeme düşmesi",
    "0150": "Kazı/Tarama sırasında malzeme düşmesi",
    "0160": "Topuk/Ayak göçüğü",
    "0170": "Açık ocak şev kayması/göçüğü",
    "0180": "Dolgu/Tavan arası çökmesi",
    "0190": "Diğer göçük/düşme olayları",
    # ── 02 — Malzeme Düşmesi / Sıkışma ──
    "0210": "Malzeme arası sıkışma (el/parmak)",
    "0220": "Makine/ekipman parçası düşmesi",
    "0230": "Malzeme taşırken düşme/sıkışma",
    "0240": "Dönen/hareketli parçaya sıkışma",
    "0250": "Düşen alet/malzeme çarpması",
    "0260": "Yükleme/boşaltma sırasında ezilme",
    "0270": "Diğer malzeme düşmesi/sıkışma",
    # ── 03 — Düşme (Kişi) ──
    "0310": "Kayarak düşme (aynı seviye)",
    "0320": "Yüksekten düşme",
    "0330": "Boşluğa/Kanala düşme",
    "0340": "Araçtan/ekipmandan düşme",
    "0350": "Merdiven/İskele düşmesi",
    "0360": "Kuyu/Eğimli kuyu içi düşme",
    "0370": "Tökezleme/Takılma",
    "0380": "Diğer kişi düşme olayları",
    # ── 04 — Çarpma / Vurma ──
    "0410": "Alete/Malzemeye çarpma",
    "0420": "Makine/Kepçe çarpması",
    "0430": "Tel/Çapak batması",
    "0440": "Sabit nesneye çarpma",
    "0450": "Fırlayan/Sıçrayan parça",
    "0460": "Basınçlı madde sıçraması",
    "0470": "Diğer çarpma/vurma olayları",
    # ── 05 — Motorlu Nakliye / Araç Kazası ──
    "0510": "Servis aracı kazası",
    "0520": "İş makinası kazası",
    "0530": "Maden kamyonu kazası",
    "0540": "Servise binerken/inerken kaza",
    "0550": "Konveyör/Bant kazası",
    "0560": "Vagon/Araba kayması/çarpması",
    "0570": "LHD/Shuttle Car kazası",
    "0580": "Yaya-araç çarpışması",
    "0590": "Diğer motorlu nakliye kazaları",
    # ── 06 — Motorsuz Nakliye ──
    "0610": "El arabası/Manuel vagon kazası",
    "0620": "Kızak/Oluk kayması",
    "0630": "Diğer motorsuz nakliye",
    # ── 07 — Vinç / Kaldırma ──
    "0710": "Kafes/Skip kazası",
    "0720": "Vinç halatı/Zincir kopması",
    "0730": "Kaldırma sırasında yük düşmesi",
    "0740": "Kuyu dibi/başı kazası",
    "0750": "Diğer vinç/kaldırma kazaları",
    # ── 08 — Makine Kazası ──
    "0810": "Kazı makinesi kazası",
    "0820": "Matkap/Delici kazası",
    "0830": "Kompresör/Pompa kazası",
    "0840": "Kırıcı/Öğütücü kazası",
    "0850": "Kaynak makinesi kazası",
    "0860": "Hidrolik sistem arızası",
    "0870": "Tahkimat ekipmanı arızası",
    "0880": "Diğer makine kazaları",
    # ── 09 — Elektrik Kazası ──
    "0910": "Elektrik çarpması",
    "0920": "Elektrik arkı/Yanığı",
    "0930": "Statik elektrik kaynaklı kaza",
    "0940": "Diğer elektrik kazaları",
    # ── 10 — Gaz veya Toz Tutuşması / Patlaması ──
    "1010": "Grizu (Metan) patlaması",
    "1020": "Kömür tozu patlaması",
    "1030": "Gaz sızıntısı / Zehirlenmesi",
    "1040": "Ani gaz püskürmesi (Outburst)",
    "1050": "Diğer gaz/toz olayları",
    # ── 11 — Yangın ──
    "1110": "Yeraltı yangını",
    "1120": "Yerüstü yangını",
    "1130": "Konveyör bant yangını",
    "1140": "Diğer yangın olayları",
    # ── 12 — Su Baskını / İnündasyon ──
    "1210": "Yeraltı su baskını",
    "1220": "Çamur/Kum akması",
    "1230": "Yüzey suyu taşkını",
    "1240": "Diğer su/sıvı baskınları",
    # ── 13 — Patlayıcı Madde Kazası ──
    "1310": "Dinamit atışı sırasında kaza",
    "1320": "Patlayıcı depolama/taşıma kazası",
    "1330": "Ateşleme sistemi arızası",
    "1340": "Sıkışmış patlayıcı (misfire)",
    "1350": "Diğer patlayıcı madde kazaları",
    # ── 14 — Elle Taşıma / Zorlanma ──
    "1410": "Bel incitmesi/Zorlanma",
    "1420": "Kas/Eklem zorlanması",
    "1430": "İtme/Çekme zorlanması",
    "1440": "Tekrarlayan hareket yaralanması",
    "1450": "Diğer zorlanma olayları",
    # ── 15 — Mahsur Kalma ──
    "1510": "Göçük sonrası mahsur kalma",
    "1520": "Yangın/Gaz nedeniyle mahsur kalma",
    "1530": "Su baskını nedeniyle mahsur kalma",
    "1540": "Diğer mahsur kalma olayları",
    # ── 16 — Sıcak Çalışma / Kaynak ──
    "1610": "Kaynak dumanı/Kıvılcım",
    "1620": "Sıcak yüzey yanığı",
    "1630": "Kesme/Taşlama sırasında kaza",
    "1640": "Diğer sıcak çalışma kazaları",
    # ── 17 — Sağlık Sorunu (İşyerinde) ──
    "1710": "Baş dönmesi/Tansiyon/Bilinç kaybı",
    "1720": "Mide/Karın ağrısı",
    "1730": "Göğüs/Kalp sıkışması",
    "1740": "Ateş/Üşüme/Halsizlik",
    "1750": "Sara nöbeti / Epilepsi",
    "1760": "Isı çarpması / Hipotermi",
    "1770": "Toz maruziyeti (Akut)",
    "1780": "Gürültü maruziyeti (Akut)",
    "1790": "Diğer sağlık sorunları",
    # ── 18 — Basınçlı Kap Patlaması ──
    "1810": "Hava hortumu/Tankı patlaması",
    "1820": "Hidrolik hat/Hortum patlaması",
    "1830": "Lastik patlaması",
    "1840": "Diğer basınçlı kap patlamaları",
    # ── 19 — El Aletleri Kazası ──
    "1910": "Kesici alet yaralanması",
    "1920": "Vurma aleti kayması",
    "1930": "Anahtar/Somun sıkma kazası",
    "1940": "Diğer el aleti kazaları",
    # ── 20 — Diğer Nedenler ──
    "2010": "Hayvan/Böcek saldırısı",
    "2020": "Doğal afet etkisi",
    "2030": "Şiddet/Saldırı",
    "2040": "Nesneye basma",
    "2050": "Kimyasal madde teması",
    "2060": "Radyasyon maruziyeti",
    "2090": "Diğer (Sınıflandırılamayan)",
}


def decode_neden(raw_code) -> str:
    """Neden kodunu önce tam eşleşme, sonra grup eşleşmesiyle çöz."""
    if pd.isna(raw_code):
        return "Belirtilmemiş"
    code_str = str(int(raw_code)).zfill(4)   # 311 → "0311", 1150 → "1150"

    # 1) Tam eşleşme
    if code_str in NEDEN_MAP:
        return NEDEN_MAP[code_str]

    # 2) Grup eşleşmesi (ilk 2 basamak)
    group = code_str[:2]
    matches = [(k, v) for k, v in NEDEN_MAP.items() if k.startswith(group)]
    if matches:
        return f"{matches[0][1]} (Kod:{int(raw_code)})"

    return f"Kod: {int(raw_code)}"


# ─────────────────────────────────────────────────────────────────────────────
# VERİTABANI YARDIMCILARI
# ─────────────────────────────────────────────────────────────────────────────

def get_conn():
    return psycopg2.connect(**DB)


def now_utc() -> datetime:
    return datetime.utcnow()


# ─────────────────────────────────────────────────────────────────────────────
# PERSONNEL
# ─────────────────────────────────────────────────────────────────────────────

def upsert_personnel(cur, row: pd.Series, dry_run: bool) -> str | None:
    """
    TKIId (Sicil No) ile personeli ara; yoksa ekle.
    Personel ID'sini (UUID string) döndürür.
    """
    tkiid = str(row["Sicil No"]).strip().replace(".0", "")

    # Mevcut mi?
    cur.execute('SELECT "Id" FROM "Personnels" WHERE "TKIId" = %s', (tkiid,))
    existing = cur.fetchone()
    if existing:
        return str(existing[0])

    # Meslek decode (global TIS_MAP kullan)
    sanat = row["Sanatı"]
    profession = (
        TIS_MAP.get(int(sanat), f"Kod: {int(sanat)}")
        if pd.notna(sanat) else None
    )

    # Doğum tarihi: ya sadece yıl (1988) ya da tam tarih (1975-06-27)
    born_raw = row["Doğum-tarihi"]
    born_date: date | None = None
    if pd.notna(born_raw):
        if isinstance(born_raw, (datetime, pd.Timestamp)):
            born_date = born_raw.date()          # Tam tarih → olduğu gibi kullan
        else:
            born_date = date(int(born_raw), 1, 1)  # Sadece yıl → 1 Ocak

    pid = str(uuid.uuid4())
    if not dry_run:
        cur.execute(
            """
            INSERT INTO "Personnels"
              ("Id", "TKIId", "Name", "Surname",
               "BornDate", "Profession", "Directorate",
               "CreatedDate", "UpdatedDate")
            VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s)
            """,
            (
                pid,
                tkiid,
                str(row["Adı"]).strip()   if pd.notna(row["Adı"])   else None,
                str(row["Soyadı"]).strip() if pd.notna(row["Soyadı"]) else None,
                born_date,
                profession,
                str(row["İşletme"]).strip() if pd.notna(row["İşletme"]) else None,
                now_utc(), now_utc(),
            ),
        )
    return pid


# ─────────────────────────────────────────────────────────────────────────────
# ACCIDENT
# ─────────────────────────────────────────────────────────────────────────────

def insert_accident(
    cur,
    row: pd.Series,
    personnel_id: str,
    dry_run: bool,
) -> bool:
    """
    Kaza kaydı ekle.
    Aynı personel + tarih + saat kombinasyonu varsa atla.
    True döner → eklendi, False → atlandı.
    """
    acc_date = row["Kaza-tarihi"]
    if pd.isna(acc_date):
        return False

    acc_date_val = pd.to_datetime(acc_date).date()

    # Saat string normalize (timedelta olabilir)
    saat_raw = row["Saat"]
    if pd.isna(saat_raw):
        saat_str = None
    elif hasattr(saat_raw, "components"):          # timedelta
        h = int(saat_raw.components.hours)
        m = int(saat_raw.components.minutes)
        saat_str = f"{h:02d}:{m:02d}"
    else:
        saat_str = str(saat_raw)[:5]               # "10:00:00" → "10:00"

    # Duplicate check (PersonnelId + AccidentDate + AccidentHour)
    cur.execute(
        """
        SELECT "Id" FROM "Accidents"
        WHERE "PersonnelId" = %s AND "AccidentDate" = %s AND "AccidentHour" = %s
        """,
        (personnel_id, acc_date_val, saat_str),
    )
    if cur.fetchone():
        return False   # Zaten var

    # Decode alanlar (global map'leri kullan)
    yer_code  = row["Yer"]
    yer_name  = YER_MAP.get(int(yer_code), f"Kod: {int(yer_code)}") if pd.notna(yer_code) else "Belirtilmemiş"

    neden_name = decode_neden(row["Neden"])  # Artık parametre almaz

    uzuv_code  = row["Uzuv"]
    uzuv_name  = UZUV_MAP.get(int(uzuv_code), f"Kod: {int(uzuv_code)}") if pd.notna(uzuv_code) else "Belirtilmemiş"

    gun_kaybi  = row["Gün-Kayıbı"]
    lost_days  = int(gun_kaybi) if pd.notna(gun_kaybi) else None

    aciklama   = str(row["Kazanın Kısa Açıklaması"]).strip() if pd.notna(row["Kazanın Kısa Açıklaması"]) else None

    aid = str(uuid.uuid4())
    if not dry_run:
        cur.execute(
            """
            INSERT INTO "Accidents"
              ("Id", "PersonnelId", "AccidentDate", "AccidentHour",
               "AccidentArea", "TypeOfAccident", "Limb",
               "LostDayOfWork", "Description",
               "CreatedDate", "UpdatedDate")
            VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
            """,
            (
                aid,
                personnel_id,
                acc_date_val,
                saat_str,
                yer_name,
                neden_name,
                uzuv_name,
                lost_days,
                aciklama,
                now_utc(), now_utc(),
            ),
        )
    return True


# ─────────────────────────────────────────────────────────────────────────────
# ACCIDENT STATISTIC (Yevmiye)
# ─────────────────────────────────────────────────────────────────────────────

def insert_statistic(cur, row: pd.Series, dry_run: bool) -> bool:
    """
    Aylık yevmiye/istatistik kaydı ekle.
    Aynı Yıl + Ay + İşletme varsa atla.
    """
    yil   = str(int(row["Yıl"])) if pd.notna(row["Yıl"]) else None
    ay_tr = str(row["Ay"]).strip() if pd.notna(row["Ay"]) else None
    islt  = str(row["İşletme"]).strip() if pd.notna(row["İşletme"]) else None

    if not yil or not ay_tr or not islt:
        return False

    ay = MONTHS_TR.get(ay_tr)
    if not ay:
        return False

    # Duplicate check
    cur.execute(
        """
        SELECT "Id" FROM "AccidentStatistics"
        WHERE "Year" = %s AND "Month" = %s AND "Directorate" = %s
        """,
        (yil, ay, islt),
    )
    if cur.fetchone():
        return False

    def safe_int(val):
        return int(val) if pd.notna(val) else None

    sid = str(uuid.uuid4())
    if not dry_run:
        cur.execute(
            """
            INSERT INTO "AccidentStatistics"
              ("Id", "Year", "Month", "Directorate",
               "ActualDailyWageUnderground", "ActualDailyWageSurface",
               "EmployeesNumberUnderground", "EmployeesNumberSurface",
               "CreatedDate", "UpdatedDate")
            VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
            """,
            (
                sid, yil, ay, islt,
                safe_int(row.get("Yeraltı Yevmiye")),
                safe_int(row.get("Yerüstü Yevmiye")),
                safe_int(row.get("Yeraltı İşçi")),
                safe_int(row.get("Yerüstü İşçi")),
                now_utc(), now_utc(),
            ),
        )
    return True


# ─────────────────────────────────────────────────────────────────────────────
# MAIN
# ─────────────────────────────────────────────────────────────────────────────

def main():
    parser = argparse.ArgumentParser(description="OHS Excel → PostgreSQL import")
    parser.add_argument("--dry-run", action="store_true",
                        help="Veritabanına yazmadan sonuçları göster")
    parser.add_argument("--mode", choices=["veri", "yevmiye", "both"], default="both",
                        help="Import modu: veri | yevmiye | both (varsayılan: both)")
    args = parser.parse_args()
    dry_run = args.dry_run
    mode = args.mode

    if dry_run:
        print("⚠️  KUR ÇALIŞTIRIM (dry-run) — veritabanına yazılmayacak\n")

    print(f"📌 Mod: {mode.upper()}\n")

    # ── Sözlükler ─────────────────────────────────────────────────────────────
    # Hardcoded sözlükler dosya başında tanımlı (TIS_MAP, YER_MAP, NEDEN_MAP)
    if mode in ("veri", "both"):
        print("\n📖 Kodlama sözlükleri hazır")
        print(f"   TİS (Sanatı): {len(TIS_MAP)} kod  |  Yer: {len(YER_MAP)} kod  |  Neden: {len(NEDEN_MAP)} kod")

    # ── Veri.xlsx ─────────────────────────────────────────────────────────────
    df_veri = None
    if mode in ("veri", "both"):
        print("\n📂 Veri.xlsx okunuyor...")
        df_veri = pd.read_excel(VERI_FILE)
        print(f"   {len(df_veri)} satır bulundu")

    # Yer kodları 23-26 Kodlama'da yok (eski sistem) → fallback tanımla (global dict'e ekle)
    YER_MAP.update({
        23: "Yerüstü Tesis / Ocak Sahası (Kod:23)",
        24: "Açık Ocak / Pasa Sahası (Kod:24)",
        25: "Atölye / Garaj / Depo (Kod:25)",
        26: "Diğer Yerüstü (Kod:26)",
    })

    # Eşleşemeyen kodları kontrol et (bilgilendirici)
    unmatched_tis = set()
    for _, row in df_veri.iterrows():
        if pd.notna(row["Sanatı"]) and int(row["Sanatı"]) not in TIS_MAP:
            unmatched_tis.add(int(row["Sanatı"]))

    if unmatched_tis:
        print(f"   ⚠️  TİS (Sanatı) eşleşemeyen kodlar: {sorted(unmatched_tis)}")
    print(f"   ℹ️  Uzuv 1-8 sistemi manuel haritalama kullanılıyor")

    # ── Yevmiye ───────────────────────────────────────────────────────────────
    df_yev = None
    if mode in ("yevmiye", "both"):
        print("\n📂 Veri Yevmiye.xlsx okunuyor...")
        df_yev = pd.read_excel(YEVMIYE_FILE)
        print(f"   {len(df_yev)} satır bulundu")

    # ── Bağlantı ──────────────────────────────────────────────────────────────
    print("\n🔌 PostgreSQL bağlantısı kuruluyor...")
    try:
        conn = get_conn()
    except Exception as e:
        print(f"❌ Bağlantı hatası: {e}")
        sys.exit(1)

    cur = conn.cursor()
    cur.execute("SELECT version()")
    print(f"   {cur.fetchone()[0][:60]}")

    # ── Veri.xlsx import ──────────────────────────────────────────────────────
    p_created = p_skipped = a_created = a_skipped = a_error = 0
    if df_veri is not None:
      print(f"\n{'[DRY-RUN] ' if dry_run else ''}🚀 Veri.xlsx aktarılıyor...")

    if df_veri is not None:
        # Duplicate kontrolü: TKIId'leri önceden yükle
        existing_tkiids: set[str] = set()
        if not dry_run:
            cur.execute('SELECT "TKIId" FROM "Personnels"')
            existing_tkiids = {r[0] for r in cur.fetchall() if r[0]}

        for idx, row in df_veri.iterrows():
            try:
                tkiid = str(row["Sicil No"]).strip().replace(".0", "")
                if tkiid in existing_tkiids:
                    p_skipped += 1
                    if not dry_run:
                        cur.execute('SELECT "Id" FROM "Personnels" WHERE "TKIId" = %s', (tkiid,))
                        pid = str(cur.fetchone()[0])
                    else:
                        pid = str(uuid.uuid4())
                else:
                    pid = upsert_personnel(cur, row, dry_run)
                    existing_tkiids.add(tkiid)
                    p_created += 1

                inserted = insert_accident(cur, row, pid, dry_run)
                if inserted:
                    a_created += 1
                else:
                    a_skipped += 1

                if not dry_run and idx % 100 == 0:
                    conn.commit()
                    print(f"   ... {idx}/{len(df_veri)} satır işlendi")

            except Exception as e:
                a_error += 1
                print(f"   ⚠️  Satır {idx} hatası: {e}")
                if not dry_run:
                    conn.rollback()

        if not dry_run:
            conn.commit()

        print(f"\n   Personel  → Eklendi: {p_created}  |  Atlandı (var): {p_skipped}")
        print(f"   Kaza      → Eklendi: {a_created}  |  Atlandı (var): {a_skipped}  |  Hata: {a_error}")

    # ── Yevmiye import ────────────────────────────────────────────────────────
    s_created = s_skipped = s_error = 0
    if df_yev is not None:
        print(f"\n{'[DRY-RUN] ' if dry_run else ''}🚀 Veri Yevmiye.xlsx aktarılıyor...")

        for idx, row in df_yev.iterrows():
            try:
                inserted = insert_statistic(cur, row, dry_run)
                if inserted:
                    s_created += 1
                else:
                    s_skipped += 1
            except Exception as e:
                s_error += 1
                print(f"   ⚠️  Satır {idx} hatası: {e}")
                if not dry_run:
                    conn.rollback()

        if not dry_run:
            conn.commit()

        print(f"\n   İstatistik → Eklendi: {s_created}  |  Atlandı (var): {s_skipped}  |  Hata: {s_error}")

    cur.close()
    conn.close()

    # ── Özet ──────────────────────────────────────────────────────────────────
    print("\n" + "─" * 55)
    print("✅ TAMAMLANDI" + (" (KUR ÇALIŞTIRIM)" if dry_run else ""))
    print(f"   Personel   : {p_created} eklendi")
    print(f"   Kaza       : {a_created} eklendi")
    print(f"   İstatistik : {s_created} eklendi")
    if dry_run:
        print("\n   Gerçek import için: python3 import_data.py")
    print("─" * 55)


if __name__ == "__main__":
    main()
