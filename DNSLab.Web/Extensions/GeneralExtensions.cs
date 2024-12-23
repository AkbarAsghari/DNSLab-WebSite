using MD.PersianDateTime.Standard;

namespace DNSLab.Web.Extensions
{
    public static class GeneralExtensions
    {
        public static string Separate3Digits(this long value) => value.ToString("N0");

        public static string Separate3Digits(this int value) => Separate3Digits(value);

        public static string ToPersianDateTime(this DateTime value) => new PersianDateTime(value).ToLongDateTimeString();
    }
}
