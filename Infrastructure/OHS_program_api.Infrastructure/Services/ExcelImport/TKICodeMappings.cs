namespace OHS_program_api.Infrastructure.Services.ExcelImport
{
    /// <summary>
    /// TKI kodlamalarını gerçek değerlere çeviren mapping sınıfı
    /// </summary>
    public static class TKICodeMappings
    {
        // Meslek kodlamaları
        public static readonly Dictionary<int, string> Professions = new Dictionary<int, string>
        {
            { 1, "İşçi (Yeraltı)" },
            { 2, "Kazmacı" },
            { 3, "Tahkimatçı (Y.tah.opr.dahil)" },
            { 4, "Tamir-bakım (Mekanik)" },
            { 5, "Tamir-bakım (Elektrik)" },
            { 6, "Ajustör" },
            { 7, "Tamir-tarama" },
            { 8, "İhzaratçı" },
            { 9, "Konveyör yol" },
            { 10, "Vinççi-sacci (Yerkars-tekkars dahil)" },
            { 11, "Sürücü-kancacı" },
            { 12, "Marangoz (Yol marangozu dahil)" },
            { 13, "Ateşleyici" },
            { 14, "Nezaretçi" },
            { 15, "Diğer yeraltı meslekler" },
            { 51, "İşçi (Yerüstü)" },
            { 52, "Manevracı-harmancı" },
            { 53, "Aşçı-garson-odacı-bahçıvan vs." },
            { 54, "Tamir-bakım-imalat (Mekanik)" },
            { 55, "Tamir-bakım-imalat (Yeraltı)" },
            { 56, "Şoför" },
            { 57, "İş makinası operatörü" },
            { 58, "Yağcı" },
            { 59, "Dökümcü-kaynakçı" },
            { 60, "İnşaat işçisi" },
            { 61, "Döşemeci-lastik tamircisi-kaportacı" },
            { 62, "Marangoz-hızarcı" },
            { 63, "Sıhhi tesisatçı-kaloriferci" },
            { 64, "Nezaretçi" },
            { 65, "Diğer yerüstü meslekler" }
        };

        // Kaza yerleri kodlamaları
        public static readonly Dictionary<int, string> AccidentAreas = new Dictionary<int, string>
        {
            { 11, "Ayak içi" },
            { 12, "Yeraltı hazırlıklar" },
            { 13, "Diğer yeraltı ocak yolları" },
            { 21, "Atölyeler (hizar ve demirci dahil)" },
            { 22, "Kriblaj, lavvar (elek, tumba vs. dahil)" },
            { 23, "Ambarlar" },
            { 24, "Sosyal tesisler (misafirhane, bürolar, yemekhane vs.)" },
            { 25, "Karo sahası" },
            { 26, "Açık ocaksahası" }
        };

        // Kaza türleri kodlamaları
        public static readonly Dictionary<int, string> TypeOfAccidents = new Dictionary<int, string>
        {
            { 100, "Gazdan boğulma veya zehirlenme" },
            { 200, "Gaz veya toz patlaması" },
            { 311, "Ayakta kazı yaparken göçük, taş veya kömür düşmesi" },
            { 312, "Ayakta arkadan kömür çekerken göçük, taş veya kömür düşmesi" },
            { 313, "Ayakta tahkimat yaparken veya sökerken (tamir-tarama dahil) göçük, taş veya kömür düşmesi" },
            { 314, "Ayakta temizlik yaparken, malzeme çekerken ve diğer durumlarda göçük, taş veya kömür düşmesi" },
            { 315, "Ayak montajı-demontajı sırasında göçük, taş veya kömür düşmesi" },
            { 321, "Hazırlıklarda kazı yaparken göçük, taş veya kömür düşmesi" },
            { 322, "Hazırlıklarda tahkimat yaparken veya sökerken (tamir-tarama dahil) göçük, taş veya kömür düşmesi" },
            { 323, "Hazırlıklarda kömür veya taş yüklerken göçük, taş veya kömür düşmesi" },
            { 324, "Hazırlıklarda diğer durumlarda göçük, taş veya kömür düşmesi" },
            { 331, "Ocak yollarında tamir-tarama yaparken göçük, taş veya kömür düşmesi" },
            { 332, "Ocak yollarında yürürken, beklerken ve diğer durumlarda göçük, taş veya kömür düşmesi" },
            { 411, "Tahkimatı kurarken tahkimat malzemesi düşmesi" },
            { 412, "Tahkimatı sökerken tahkimat malzemesi düşmesi" },
            { 413, "Kurulu tahkimatın düşmesi" },
            { 421, "Bir şeyin yüksekten üzerine düşmesi (nakliyat, tamir, bakım, montaj-demontaj hariç)" },
            { 422, "Tuttuğu bir şeyin elinden kurtulup düşmesi (el aletleri hariç) (nakliyat, tamir, bakım, montaj-demontaj hariç)" },
            { 423, "Diğer düşmeler (nakliyat, tamir, bakım, montaj-demontaj hariç)" },
            { 510, "Hareket halindeki bir şeyin çarpması, vurması (nakliyat, makine, iş makinesi el aletleri hariç)" },
            { 521, "Yeraltında tahkimat parçalarına çarpmak" },
            { 522, "Yeraltında nakliyat ünitelerine çarpmak" },
            { 523, "Diğer duran bir malzemeye vurmak, çarpmak" },
            { 530, "Diğer çarpmalar" },
            { 600, "Patlayıcı madde kazaları" },
            { 710, "Herhangi bir şeyi taşırken, kaldırırken, indirirken üzerine düşmesi" },
            { 720, "Herhangi bir şeyi taşırken, kaldırırken, indirirken düşmek" },
            { 730, "Herhangi bir şeyi taşırken, kaldırırken, indirirken uzuv sıkışması" },
            { 740, "Herhangi bir şeyi taşırken, kaldırırken, indirirken belini incitmek" },
            { 750, "Diğer elle taşıma kazaları" },
            { 810, "Konveyörlerin hareketli aksamlarının çarpması, vurması, sıkıştırması" },
            { 820, "Vagon ve lokomotifin çarpması, vurması, sıkıştırması" },
            { 830, "Diğer nakliyat ünitelerinin çarpması, vurması, sıkıştırması" },
            { 840, "Nakliyat ünitelerinin üzerine bir şey düşmesi" },
            { 850, "Düşen vagonu kaldırırken bel incinmesi" },
            { 860, "İnsan nakliyatı sırasında nakliyat ünitesinden düşmek, çarpmak, sıkışmak" },
            { 870, "Diğer mekanik taşıma kazaları" },
            { 911, "İş makinelerinin devrilmesiyle oluşan kazalar" },
            { 912, "İş makinelerinin çarpmasıyla oluşan kazalar" },
            { 913, "İş makinelerinin yanmasıyla oluşan kazalar" },
            { 921, "Otomobil, otobüs, pikap vs. devrilmesiyle oluşan kazalar" },
            { 922, "Otomobil, otobüs, pikap vs. çarpmasıyla oluşan kazalar" },
            { 923, "Otomobil, otobüs, pikap vs. yanmasıyla oluşan kazalar" },
            { 930, "Diğer karayolu ile taşıma sonucu oluşan kazalar" },
            { 1010, "Elektrik çarpması" },
            { 1020, "Elektrik araç gereç, motor, hat vs. tamir bak., mont.-demon. sırasında ve diğer elektrik çarpmaları" },
            { 1110, "Yerüstü iş makinesi dışındaki makine veya bir parçasının üzerine düşmesi" },
            { 1120, "Yerüstü iş makinesi dışındaki makine veya bir parçasının çarpması, vurması" },
            { 1130, "Basınçlı hava veya hidrolik hortum patlatması" },
            { 1140, "Atölyelerdeki tamir, bakım vs. sırasında kayarak düşme" },
            { 1150, "Yerüstü iş makinesi dışındaki makinelerle çalışırken uzuv sıkışması" },
            { 1160, "Yerüstü iş makinesi dışındaki makineler ile çalışırken uzuvlara çapak, yonga vs. batması (çivi batması dışında)" },
            { 1170, "Makine parçasını kaldırırken veya zorlanma sonucu belini incitme" },
            { 1180, "Diğer yerüstü iş makinesi dışındaki makine kazaları" },
            { 1210, "Yerüstü iş makinesi parçasının üzerine düşmesi" },
            { 1220, "Yerüstü iş makinesinin çarpması, vurması" },
            { 1230, "Yerüstü iş makinesinin üzerinde çalışırken düşme" },
            { 1240, "Yerüstü iş makinesine binerken veya inerken merdivenden düşme" },
            { 1250, "Yerüstü iş makinesinde çalışırken uzuv sıkışması" },
            { 1260, "Yerüstü iş makinesi ile çalışırken uzuvlara çapak, yonga vs. batması (çivi batması dışında)" },
            { 1270, "Yerüstü iş makinesi parçasını kaldırma veya zorlama sonucu belini incitme" },
            { 1280, "Yerüstü iş makinesi ile çalışırken gerçekleşen diğer kazalar" },
            { 1310, "El aleti ile kendisine vurmak, kesmek, düşürmek" },
            { 1320, "El aleti ile çalışırken zorlanma sonucu belini incitmek" },
            { 1330, "El aleti ile çalışırken zorlanma sonucu düşmek" },
            { 1340, "El aleti ile çalışırken gerçekleşen diğer kazalar" },
            { 1411, "Yürürken düşme" },
            { 1412, "Merdivenden inerken veya çıkarken düşme (iş makineleri dışında)" },
            { 1413, "Herhangi bir yükseklikten düşme (iş makineleri dışında)" },
            { 1414, "Diğer düşmeler" },
            { 1420, "Tahkimatı yaparken veya sökerken uzuv sıkışması" },
            { 1430, "Kapıya el, ayak vs. sıkışması" },
            { 1440, "Tahkimat yaparken veya sökerken bel incinmesi" },
            { 1450, "Ayak burkulması" },
            { 1460, "Çivi batması" },
            { 1470, "Diğer kazalar" }
        };

        // Uzuv kodlamaları
        public static readonly Dictionary<int, string> Limbs = new Dictionary<int, string>
        {
            { 1, "Baş" },
            { 2, "El" },
            { 3, "Ayak" },
            { 4, "Kol" },
            { 5, "Bacak" },
            { 6, "Gövde" },
            { 7, "Muhtelif" },
            { 8, "Ölü" }
        };

        // Ay isimlerini numaraya çeviren mapping
        public static readonly Dictionary<string, string> MonthNames = new Dictionary<string, string>
        {
            { "Ocak", "01" },
            { "Şubat", "02" },
            { "Mart", "03" },
            { "Nisan", "04" },
            { "Mayıs", "05" },
            { "Haziran", "06" },
            { "Temmuz", "07" },
            { "Ağustos", "08" },
            { "Eylül", "09" },
            { "Ekim", "10" },
            { "Kasım", "11" },
            { "Aralık", "12" }
        };

        /// <summary>
        /// Meslek kodunu ismine çevirir
        /// </summary>
        public static string GetProfessionName(int code)
        {
            return Professions.TryGetValue(code, out var name) ? name : "Geçersiz Kod";
        }

        /// <summary>
        /// Kaza yeri kodunu ismine çevirir
        /// </summary>
        public static string GetAccidentAreaName(int code)
        {
            return AccidentAreas.TryGetValue(code, out var name) ? name : "Geçersiz Kod";
        }

        /// <summary>
        /// Kaza türü kodunu ismine çevirir
        /// </summary>
        public static string GetTypeOfAccidentName(int code)
        {
            return TypeOfAccidents.TryGetValue(code, out var name) ? name : "Geçersiz Kod";
        }

        /// <summary>
        /// Uzuv kodunu ismine çevirir
        /// </summary>
        public static string GetLimbName(int code)
        {
            return Limbs.TryGetValue(code, out var name) ? name : "Geçersiz Kod";
        }

        /// <summary>
        /// Ay ismini numaraya çevirir
        /// </summary>
        public static string GetMonthNumber(string monthName)
        {
            if (string.IsNullOrEmpty(monthName)) return "00";
            
            var normalized = monthName.Trim();
            // İlk harfi büyük yap
            if (normalized.Length > 0)
            {
                normalized = char.ToUpper(normalized[0]) + normalized.Substring(1).ToLower();
            }
            
            return MonthNames.TryGetValue(normalized, out var number) ? number : "00";
        }
    }
}
