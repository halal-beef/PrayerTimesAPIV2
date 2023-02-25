using System.Net.Http.Headers;
using System.Text.Json;
using System.Globalization;
using static PrayerTimesAPIV2.Enums;

namespace PrayerTimesAPIV2
{
    public class API
    {
        private IPResponse? ip = JsonSerializer.Deserialize<IPResponse>(DownloadString("https://ipapi.co/json/"));

        #region API Funcs
        public List<Datum> PrayerTimesForYear_Month(int year, int month, calculationMethods calculationMethods, shafaq shafaq, school school, midnightMode midnightMode, latitudeAdjustmentMethod latitudeAdjustmentMethod, bool iso8601Output)
        {
            string shafaqParsed = "general";

            switch(shafaq)
            {
                case shafaq.general:
                    shafaqParsed = "general";
                    break;
                case shafaq.ahmer:
                    shafaqParsed = "ahmer";
                    break;
                case shafaq.abyad:
                    shafaqParsed = "abyad";
                    break;
            }

            string response = DownloadString($"http://api.aladhan.com/v1/calendar/{year}/{month}?latitude={ip.latitude}&longitude={ip.longitude}&method={calculationMethods}&shafaq={shafaqParsed}&school={school}&midnightMode={midnightMode}&latitudeAdjustmentMethod={latitudeAdjustmentMethod}&iso8601={iso8601Output.ToString().ToLower()}");

            prayerTimeResponse? resp = JsonSerializer.Deserialize<prayerTimeResponse>(response);
            return resp.data;
        }

        public Timings PrayerTimesForToday(calculationMethods calculationMethods, shafaq shafaq, school school, midnightMode midnightMode, latitudeAdjustmentMethod latitudeAdjustmentMethod, bool iso8601Output)
        {
            string shafaqParsed = "general";

            switch (shafaq)
            {
                case shafaq.general:
                    shafaqParsed = "general";
                    break;
                case shafaq.ahmer:
                    shafaqParsed = "ahmer";
                    break;
                case shafaq.abyad:
                    shafaqParsed = "abyad";
                    break;
            }

            string response = DownloadString($"http://api.aladhan.com/v1/calendar/{DateTime.Now.Year}/{DateTime.Now.Month}?latitude={ip.latitude}&longitude={ip.longitude}&method={calculationMethods}&shafaq={shafaqParsed}&school={school}&midnightMode={midnightMode}&latitudeAdjustmentMethod={latitudeAdjustmentMethod}&iso8601={iso8601Output.ToString().ToLower()}");

            prayerTimeResponse? resp = JsonSerializer.Deserialize<prayerTimeResponse>(response);
            return resp.data.Where(x => x.date.gregorian.date == DateTime.Today.ToString("dd-MM-yyyy")).First().timings;
        }

        public Timings PrayerTimesForDay(string day, calculationMethods calculationMethods, shafaq shafaq, school school, midnightMode midnightMode, latitudeAdjustmentMethod latitudeAdjustmentMethod, bool iso8601Output)
        {
            string shafaqParsed = "general";

            switch (shafaq)
            {
                case shafaq.general:
                    shafaqParsed = "general";
                    break;
                case shafaq.ahmer:
                    shafaqParsed = "ahmer";
                    break;
                case shafaq.abyad:
                    shafaqParsed = "abyad";
                    break;
            }

            DateTime dt;
            DateTime.TryParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            string response = DownloadString($"http://api.aladhan.com/v1/calendar/{dt.Year}/{dt.Month}?latitude={ip.latitude}&longitude={ip.longitude}&method={calculationMethods}&shafaq={shafaqParsed}&school={school}&midnightMode={midnightMode}&latitudeAdjustmentMethod={latitudeAdjustmentMethod}&iso8601={iso8601Output.ToString().ToLower()}");

            prayerTimeResponse? resp = JsonSerializer.Deserialize<prayerTimeResponse>(response);
            return resp.data.Where(x => x.date.gregorian.date == dt.ToString("dd-MM-yyyy")).First().timings;
        }
        #endregion

        #region Misc Method
        private static string DownloadString(string Url)
        {
            HttpClient hc = new();
            hc.Timeout = Timeout.InfiniteTimeSpan;
            hc.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("PrayerTimesAPIV2", "1"));
            HttpResponseMessage hrm4 = hc.GetAsync(Url).GetAwaiter().GetResult();
            using (HttpContent content = hrm4.Content)
            {
                return content.ReadAsStringAsync().Result;
            }
        }
        #endregion
    }

    public class Enums
    {

        public enum calculationMethods
        {
            Shia_Ithna_Ansari = 0,
            University_of_Islamic_Sciences__Karachi = 1,
            Islamic_Society_of_North_America = 2,
            Muslim_World_League = 3,
            Umm_Al_Qura_University__Makkah = 4,
            Egyptian_General_Authority_of_Survey = 5,
            Institute_of_Geophysics__University_of_Tehran = 7,
            Gulf_Region = 8,
            Kuwait = 9,
            Qatar = 10,
            Majlis_Ugama_Islam_Singapura__Singapore = 11,
            Union_Organization_islamic_de_France = 12,
            Diyanet_İşleri_Başkanlığı, Turkey = 13,
            Spiritual_Administration_of_Muslims_of_Russia = 14,
            Moonsighting_Committee_Worldwide = 15,
            Dubai__unofficial__ = 16
        }

        public enum shafaq
        {
            general,
            ahmer,
            abyad
        }

        public enum school
        {
            Shafi = 0,
            Hanafi = 1
        }

        public enum midnightMode
        {
            Standard = 0,
            Jafari = 1
        }

        public enum latitudeAdjustmentMethod
        {
            Middle_of_the_Night = 1,
            One_Seventh = 2,
            Angle_Based = 3
        }

    }
}