using DNSLab.Web.DTOs.Repositories.Record;
using MD.PersianDateTime.Standard;
using System.Text.Json;

namespace DNSLab.Web.Extensions
{
    public static class GeneralExtensions
    {
        public static string Separate3Digits(this long value) => value.ToString("N0");

        public static string Separate3Digits(this int value) => Separate3Digits(value);

        public static string ToPersianDateTime(this DateTime value) => new PersianDateTime(value).ToLongDateTimeString();

        public static T? CastTo<T>(this BaseRecordDTO record) where T : BaseRecordDataDTO
        {
            return JsonSerializer.Deserialize<T>((JsonElement)record.RData, options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public static T Clone<T>(this T obj)
        {
            var inst = obj.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            return (T)inst?.Invoke(obj, null)!;
        }
    }
}
