# PrayerTimesAPIV2


## Features

- Better code than the old version of PrayerTimesAPI (json handling wise)

- No longer needed to be on windows 10 or above or have location services enabled

- Able to get the prayer times today

- Able to get the prayer times of a specified month & year

- Able to get the prayer times of a specified date

- Way way way more portable than the old version of PrayerTimesAPI


## Example

```csharp 

using PrayerTimesAPIV2;

API api = new();

Console.WriteLine(api.PrayerTimesForToday(Enums.calculationMethods.Umm_Al_Qura_University__Makkah, Enums.shafaq.general, Enums.school.Hanafi, Enums.midnightMode.Standard, Enums.latitudeAdjustmentMethod.Angle_Based, false).Fajr);
Console.WriteLine(api.PrayerTimesForDay("24/02/2014", Enums.calculationMethods.Umm_Al_Qura_University__Makkah, Enums.shafaq.general, Enums.school.Hanafi, Enums.midnightMode.Standard, Enums.latitudeAdjustmentMethod.Angle_Based, false).Fajr);

Console.WriteLine("\n");

foreach(Datum time in api.PrayerTimesForYear_Month(2023, 2, Enums.calculationMethods.Umm_Al_Qura_University__Makkah, Enums.shafaq.general, Enums.school.Hanafi, Enums.midnightMode.Standard, Enums.latitudeAdjustmentMethod.Angle_Based, false))
{
    Console.WriteLine($"Date: {time.date.readable}       Fajr: {time.timings.Fajr}         Sunrise: {time.timings.Sunrise}         Dhuhr: {time.timings.Dhuhr}         Asr: {time.timings.Asr}         Maghrib: {time.timings.Maghrib}          Isha: {time.timings.Isha}");
}

Thread.Sleep(-1);

```
