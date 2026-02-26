# Excel Veri İçe Aktarma

Bu klasör OHS Program için Excel dosyalarından veritabanına veri aktarma işlemlerini içerir.

## Klasör Yapısı

```
OHS_Program/                  # ← Excel dosyaları BURAYA (.gitignore ile korunur)
├── Veri.xlsx
├── Veri Yevmiye.xlsx
├── İş_Kazaları_Genişletilmiş_Kodlama.xlsx
└── OHS_project_api/
    └── ExcelDataImport/
        ├── Scripts/          # Import scripti
        │   └── import_data.py
        └── README.md         # Bu dosya
```

**Not:** Excel dosyaları git'e pushlamamak için OHS_Program ana klasöründe tutulur.

## Gereksinimler

Python paketleri:
```bash
pip install pandas openpyxl psycopg2-binary
```

## Kullanım

### 1. Kuru Çalıştırma (Önizleme)
Veritabanına yazmadan önce kontrol edin:
```bash
cd ExcelDataImport/Scripts
python3 import_data.py --dry-run
```

### 2. Gerçek İçe Aktarma
Tüm verileri içe aktarın:
```bash
python3 import_data.py
```

### 3. Sadece Belirli Verileri Aktarma
```bash
# Sadece personel ve kaza kayıtları
python3 import_data.py --mode veri

# Sadece aylık istatistikler
python3 import_data.py --mode yevmiye
```

## Özellikler

- ✅ **İdempotent**: Aynı script'i birden fazla kez çalıştırabilirsiniz, var olan kayıtların üzerine yazmaz
- ✅ **Akıllı Kodlama**: Excel'deki kodları anlamlı isimlere dönüştürür
- ✅ **Hata Toleransı**: Bir satırda hata olsa bile diğer satırları işlemeye devam eder
- ✅ **İlerleme Göstergesi**: Her 100 satırda bir ilerleme bilgisi verir

## Veritabanı Bağlantısı

Script varsayılan olarak şu ayarları kullanır:
```python
host: localhost
port: 5432
database: OHSProgramApi
user: postgres
password: 123456
```

Farklı ayarlar için `import_data.py` dosyasındaki `DB` sabitini düzenleyin.

## Dikkat

- Excel dosyalarını `ExcelFiles/` klasöründe tutun
- Veritabanı bağlantısı için PostgreSQL çalışıyor olmalı
- İlk çalıştırmadan önce `--dry-run` ile kontrol etmeniz önerilir
