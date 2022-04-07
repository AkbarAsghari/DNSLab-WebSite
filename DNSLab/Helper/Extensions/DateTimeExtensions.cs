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

        public static string ToLocalizerString(this DateTime dateTime)
        {
            DateTime localDateTime = dateTime.ToLocalTime();

            switch (CultureInfo.CurrentCulture.Name)
            {
                case "fa-FA":
                    return new PersianDateTime(localDateTime).ToString();
                case "en-EN":
                default:
                    return localDateTime.ToString();
            }
            

        }
    }
}
