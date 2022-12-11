using DNSLab.Helper.Utilities;
using MD.PersianDateTime.Standard;
using System.Globalization;

namespace DNSLab.Helper.Extensions
{
    public static class DateTimeExtensions
    {
        public static string CalcRelativeTime(this DateTime dateTime)
        {
           return RelativeTimeCalculator.Calc(dateTime);
        }

        public static string ToLocalizerString(this DateTime dateTime,bool showOnlyDate = false)
        {
            DateTime localDateTime = dateTime.ToLocalTime();

            if (showOnlyDate)
            {
                switch (CultureInfo.CurrentCulture.Name)
                {
                    case "fa-FA":
                        return new PersianDateTime(localDateTime).ToShortDateString().EnglishToPersianNumbers();
                    case "en-EN":
                    default:
                        return localDateTime.ToShortDateString();
                }
            }
            else
            {
                switch (CultureInfo.CurrentCulture.Name)
                {
                    case "fa-FA":
                        return new PersianDateTime(localDateTime).ToString().EnglishToPersianNumbers();
                    case "en-EN":
                    default:
                        return localDateTime.ToString();
                }
            }
        }
    }
}
