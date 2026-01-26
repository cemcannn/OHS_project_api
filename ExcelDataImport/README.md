# OHS Excel Data Import Sistemi

Bu klasör, TKI (Türkiye Kömür İşletmeleri) Excel formatındaki kaza verilerini ve aylık istatistik verilerini otomatik olarak sisteme aktarmak için kullanılır.

## 📁 Klasör Yapısı

```
OHS_project_api/
├── ExcelDataImport/             # Excel veri import klasörü
│   ├── ExcelFiles/              # Excel dosyalarını buraya yerleştirin
│   │   ├── Veri.xlsx           # Kaza verileri (Personel ve kaza bilgileri)
│   │   ├── Veri Yevmiye.xlsx   # Aylık istatistik verileri (Fiili yevmiye)
│   │   └── Processed/          # İşlenen dosyalar otomatik olarak buraya taşınır
│   └── README.md               # Bu dosya
├── Core/
├── Infrastructure/
├── Presentation/
└── ...
```

## 🚀 Kullanım

### 1. Excel Dosyalarını Hazırlama

#### **Veri.xlsx** (Kaza Verileri)
Excel'in **ilk sekmesinde** aşağıdaki sütunlar bulunmalıdır (tam bu sırada):

| Sütun No | Sütun Adı | Açıklama | Örnek |
|----------|-----------|----------|-------|
| 1 | Sicil No | Personelin sicil numarası | 12345 |
| 2 | Adı | Personelin adı | MEHMET |
| 3 | Soyadı | Personelin soyadı | YILMAZ |
| 4 | İşletme | İşletme adı | GLİ |
| 5 | Doğum-tarihi | DD.MM.YYYY formatında | 15.05.1985 |
| 6 | Sanatı | Meslek kodu (aşağıdaki tabloya bakın) | 1 |
| 7 | Kaza-tarihi | DD.MM.YYYY formatında | 20.01.2024 |
| 8 | Saat | Kaza saati | 14:30 |
| 9 | Yer | Kaza yeri kodu (aşağıdaki tabloya bakın) | 11 |
| 10 | Neden | Kaza türü kodu (aşağıdaki tabloya bakın) | 311 |
| 11 | Uzuv | Uzuv kodu (aşağıdaki tabloya bakın) | 2 |
| 12 | Gün-Kayıbı | İş günü kaybı | 5 |
| 13 | Kazanın Kısa Açıklaması | Açıklama metni | Ayakta çalışırken... |

#### **Veri Yevmiye.xlsx** (Aylık İstatistik Verileri)
Excel'in **ilk sekmesinde** aşağıdaki sütunlar bulunmalıdır:

| Sütun No | Sütun Adı | Açıklama | Örnek |
|----------|-----------|----------|-------|
| 1 | Yıl | Yıl | 2024 |
| 2 | Ay | Ay adı (Türkçe) | Ocak |
| 3 | İşletme | İşletme adı | GLİ |
| 4 | Yeraltı İşçi | Yeraltı işçi sayısı | 150 |
| 5 | Yerüstü İşçi | Yerüstü işçi sayısı | 80 |
| 6 | Yeraltı Yevmiye | Yeraltı fiili yevmiye | 3500 |
| 7 | Yerüstü Yevmiye | Yerüstü fiili yevmiye | 1800 |

### 2. Dosyaları Yükleme

1. Hazırladığınız Excel dosyalarını `OHS_project_api/ExcelDataImport/ExcelFiles/` klasörüne kopyalayın
2. Dosya isimleri **tam olarak** şu şekilde olmalıdır:
   - `Veri.xlsx` (kazalar için)
   - `Veri Yevmiye.xlsx` (istatistikler için)

### 3. Otomatik İşleme

- Sistem **her 60 saniyede bir** Excel dosyalarını kontrol eder
- Dosya bulunduğunda otomatik olarak okur ve veritabanına aktarır
- İşlenen dosyalar `ExcelFiles/Processed/` klasörüne tarih-saat damgası ile taşınır
- Örnek: `Veri_20240126_143052.xlsx`

### 4. Log Kontrol

API'nin log çıktılarında şu mesajları görebilirsiniz:

```
Excel Auto Import Background Service başlatıldı.
'Veri.xlsx' dosyası işleniyor...
'Veri.xlsx' başarıyla işlendi. 125 satır okundu.
Personnel Import - Eklenen: 5, Atlanan: 120
Accident Import - Eklenen: 125, Atlanan: 0
Dosya arşivlendi: Veri_20240126_143052.xlsx
```

## 📋 Kodlama Tabloları

### Meslek Kodları

| Kod | Meslek Adı |
|-----|------------|
| 1 | İşçi (Yeraltı) |
| 2 | Kazmacı |
| 3 | Tahkimatçı (Y.tah.opr.dahil) |
| 4 | Tamir-bakım (Mekanik) |
| 5 | Tamir-bakım (Elektrik) |
| 51 | İşçi (Yerüstü) |
| 52 | Manevracı-harmancı |
| 56 | Şoför |
| 57 | İş makinası operatörü |
| *...daha fazlası için kodlara bakın* |

### Kaza Yeri Kodları

| Kod | Kaza Yeri |
|-----|-----------|
| 11 | Ayak içi |
| 12 | Yeraltı hazırlıklar |
| 13 | Diğer yeraltı ocak yolları |
| 21 | Atölyeler (hizar ve demirci dahil) |
| 22 | Kriblaj, lavvar (elek, tumba vs. dahil) |
| 25 | Karo sahası |
| *...daha fazlası için kodlara bakın* |

### Kaza Türü Kodları

| Kod | Kaza Türü |
|-----|-----------|
| 100 | Gazdan boğulma veya zehirlenme |
| 200 | Gaz veya toz patlaması |
| 311 | Ayakta kazı yaparken göçük, taş veya kömür düşmesi |
| 312 | Ayakta arkadan kömür çekerken göçük, taş veya kömür düşmesi |
| 600 | Patlayıcı madde kazaları |
| 1010 | Elektrik çarpması |
| 1411 | Yürürken düşme |
| *...daha fazlası için kodlara bakın* |

### Uzuv Kodları

| Kod | Uzuv |
|-----|------|
| 1 | Baş |
| 2 | El |
| 3 | Ayak |
| 4 | Kol |
| 5 | Bacak |
| 6 | Gövde |
| 7 | Muhtelif |
| 8 | Ölü |

## ⚠️ Önemli Notlar

1. **Başlık Satırı**: Excel dosyalarının ilk satırı başlık olmalıdır (atlanır)
2. **Kodlamalar**: Sistem otomatik olarak kodları isimlere çevirir
3. **Tekrar Kontrol**: Aynı veri tekrar eklenmez (TKI ID ve tarih kontrolü yapılır)
4. **Tarih Formatı**: DD.MM.YYYY formatı kullanılmalıdır (örn: 15.01.2024)
5. **Ay İsimleri**: Türkçe olmalıdır (Ocak, Şubat, Mart, vb.)

## 🔧 Sorun Giderme

### Dosya İşlenmiyor
- Dosya ismini kontrol edin (tam eşleşmeli)
- Excel dosyasının kapalı olduğundan emin olun
- API loglarını kontrol edin

### Veriler Veritabanına Eklenmiyor
- Excel formatının doğru olduğundan emin olun
- Kodlamaların doğru olduğunu kontrol edin
- Tarih formatlarını kontrol edin

### Background Service Çalışmıyor
- API'nin çalıştığından emin olun
- `OHS_ExcelDataImport/ExcelFiles` klasörünün var olduğunu kontrol edin

## 📞 Teknik Destek

Herhangi bir sorun için lütfen geliştirme ekibi ile iletişime geçin.

---

**Son Güncelleme**: 26 Ocak 2026
**Versiyon**: 1.0.0
