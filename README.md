# OHS_program_api (Backend)

Bu repo, `OHS_program_api.sln` çözüm dosyası içindeki **ASP.NET Core Web API** projesini (`Presentation/OHS_program_api.API`) çalıştırmak içindir.

## Gereksinimler

- **.NET SDK**
  - Önerilen: **.NET 6 SDK** (proje `net6.0` hedefli).
  - Alternatif (makinede sadece yeni .NET varsa): çalıştırırken **roll-forward** kullanabilirsiniz (aşağıda).
- **PostgreSQL**
  - Uygulama veri erişimi (`OHSProgramAPIDbContext`) **Npgsql** ile PostgreSQL’e bağlanır.
  - Bağlantı metni `Presentation/OHS_program_api.API/appsettings.json` içindeki `ConnectionStrings:PostgreSQL` anahtarından okunur.

## İlk kurulum

Repo kökünde:

```bash
dotnet restore
dotnet build -c Debug
```

### PostgreSQL bağlantısı

`Presentation/OHS_program_api.API/appsettings.json` dosyasındaki `ConnectionStrings:PostgreSQL` değerini kendi ortamınıza göre düzenleyin.

> Not: Çözüm içinde `OHS_program_api.Persistence/Configurations.cs` bağlantı metnini özellikle bu `appsettings.json` dosyasından okuyor.

### Veritabanını oluşturma / migration

Migration’lar `Infrastructure/OHS_program_api.Persistence/Migrations/` altında.

```bash
dotnet ef database update --project "Infrastructure/OHS_program_api.Persistence/OHS_program_api.Persistence.csproj" --startup-project "Presentation/OHS_program_api.API/OHS_program_api.API.csproj"
```

> Eğer `dotnet ef` kurulu değilse: `dotnet tool install --global dotnet-ef`

## SuperAdmin (Super User) oluşturma

Bu projede `Development` ortamında **SuperAdmin seed** mekanizması vardır.

### En basit kullanım (Visual Studio + şifre git’e gitmesin)

1) **Nereye gideceksin?**
   - Repo kökünde bir terminal aç (Cursor terminali, VS “Terminal”, veya normal Terminal olur).

2) **Şifreyi nereye yazacaksın?**
   - Şifreyi dosyaya değil, **User Secrets** içine yazacaksın (git’e gitmez).
   - Şu komutları çalıştır:

```bash
dotnet user-secrets init --project "Presentation/OHS_program_api.API/OHS_program_api.API.csproj"
dotnet user-secrets set "SuperAdmin:Seed:Enabled" "true" --project "Presentation/OHS_program_api.API/OHS_program_api.API.csproj"
dotnet user-secrets set "SuperAdmin:Seed:Password" "Degistir_Bu_Sifreyi!" --project "Presentation/OHS_program_api.API/OHS_program_api.API.csproj"
```

3) **Sonra ne olacak?**
   - Visual Studio’da projeyi **F5** ile çalıştır.
   - SuperAdmin kullanıcı/rol yoksa oluşturulur, varsa seed tekrar çalışsa bile “yeniden yaratmaz”.

> Şifre **kodda sabit (constant) değil** ve **random üretilmiyor**: sen ne set edersen o kullanılır.

### “Git’e gitmesin” kuralı (özet)

- **YAPMA**: `launchSettings.json` / `appsettings*.json` içine şifre yazıp push etmek.
- **YAP**: Şifreyi **User Secrets** (veya env var) ile ver.

### Alternatif (terminalden tek seferlik env var ile)

```bash
SUPERADMIN_PASSWORD="Degistir_Bu_Sifreyi!" DOTNET_ROLL_FORWARD=LatestMajor \
dotnet run --project "Presentation/OHS_program_api.API/OHS_program_api.API.csproj" --launch-profile "OHS_program_api.API"
```

## Çalıştırma

### 1) Önerilen (net6 SDK kuruluysa)

```bash
dotnet run --project "Presentation/OHS_program_api.API/OHS_program_api.API.csproj" --launch-profile "OHS_program_api.API"
```

`launchSettings.json` profiline göre varsayılan adresler:
- `https://localhost:7170`
- `http://localhost:5170`

Swagger:
- `https://localhost:7170/swagger/index.html`

### 2) Alternatif (makinede sadece yeni .NET runtime/SDK varsa)

`net6.0` hedefli uygulamayı, makinede yüklü en yakın major runtime’a “roll-forward” ederek çalıştırabilirsiniz:

```bash
DOTNET_ROLL_FORWARD=LatestMajor dotnet run --project "Presentation/OHS_program_api.API/OHS_program_api.API.csproj" --launch-profile "OHS_program_api.API"
```

## HTTPS developer sertifikası (macOS)

Tarayıcı HTTPS uyarısı alırsanız:

```bash
dotnet dev-certs https --trust
```

## Kalıcı Tanımlama Seed'i

Projede tanımlama verileri için kalıcı seed altyapısı vardır.

- Seed dosyaları: `Presentation/OHS_program_api.API/Seed/Data/*.json`
- Çalışma zamanı: API açılışında otomatik
- Davranış: idempotent (sadece eksik kodları ekler, var olanları ezmez)

Seed edilen tablolar:

- `AccidentAreas`
- `TypeOfAccident`
- `Professions`
- `Limbs`

### Seed geçmişi

Uygulama ilk çalışmada `DefinitionSeedHistory` tablosunu otomatik oluşturur.
Her çalıştırmada tabloya seed özeti yazılır:

- `SeedName`
- `Version`
- `InsertedCount`
- `SkippedCount`
- `LastAppliedUtc`

Kontrol için:

```sql
SELECT * FROM "DefinitionSeedHistory" ORDER BY "SeedName";
```

### Seed'i kapatma

`appsettings.json` içine:

```json
"DefinitionSeed": {
   "Enabled": false
}
```

### Seed güncelleme adımları

1. İlgili JSON dosyasını güncelle:
    - `Seed/Data/accident_areas.json`
    - `Seed/Data/type_of_accident.json`
    - `Seed/Data/professions.json`
    - `Seed/Data/limbs.json`
2. `code` alanını mümkün olduğunca sabit tut (eşleştirme anahtarıdır).
3. Yeni kayıt gerekiyorsa yeni `code` ile ekle.
4. `Presentation/OHS_program_api.API/Seed/DefinitionSeeder.cs` dosyasında `CurrentVersion` değerini artır.
5. API'yi yeniden başlatıp `DefinitionSeedHistory` tablosundan sonucu doğrula.

Not: Bu seed yaklaşımı kullanıcı tarafından güncellenen mevcut kayıtları ezmez. Mevcut kaydı zorunlu toplu güncellemek istersen ayrı migration/patch script tercih edilmelidir.

