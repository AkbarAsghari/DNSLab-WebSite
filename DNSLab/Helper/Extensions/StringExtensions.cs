using DNSLab.Helper.Utilities;
using System.Text;

namespace DNSLab.Helper.Extensions
{
    public static class StringExtensions
    {
        static Dictionary<char, char> LettersDictionaryFaToEn = new Dictionary<char, char>() {
            {'۰', '0'},
            {'۱', '1'},
            {'۲', '2'},
            {'۳', '3'},
            {'۴', '4'},
            {'۵', '5'},
            {'۶', '6'},
            {'۷', '7'},
            {'۸', '8'},
            {'۹', '9'}
        };

        static Dictionary<char, char> LettersDictionaryEnToFa = new Dictionary<char, char>() {
            {'0', '۰'},
            {'1', '۱'},
            {'2', '۲'},
            {'3', '۳'},
            {'4', '۴'},
            {'5', '۵'},
            {'6', '۶'},
            {'7', '۷'},
            {'8', '۸'},
            {'9', '۹'}
        };

        public static string PersianToEnglishNumbers(this string persianStr)
        {
            StringBuilder sb = new StringBuilder(persianStr);
            foreach (var item in persianStr.Where(x => LettersDictionaryFaToEn.Keys.Contains(x)))
                sb.Replace(item, LettersDictionaryFaToEn[item]);
            return sb.ToString();
        }

        public static string EnglishToPersianNumbers(this string persianStr)
        {
            StringBuilder sb = new StringBuilder(persianStr);
            foreach (var item in persianStr.Where(x => LettersDictionaryEnToFa.Keys.Contains(x)))
                sb.Replace(item, LettersDictionaryEnToFa[item]);
            return sb.ToString();
        }

        public static string MakeCut(this string text, int length)
        {
            if (String.IsNullOrWhiteSpace(text))
                return text;
            return text.Length > length ? $"{text.Substring(0, length)}..." : text;
        }

        public static string ChangeUrlsToLink(this string text)
        {
            StringBuilder sb = new StringBuilder(text);

            var links = RegexTools.ExtractUrlsFromString(text);

            foreach (var link in links)
                sb.Replace(link, $"<a href=\"{link}\">{link}</a>");

            return sb.ToString();
        }
    }
}
