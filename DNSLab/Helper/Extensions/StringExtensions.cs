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
            if (persianStr.Any(x => LettersDictionaryFaToEn.Keys.Contains(x)))
            {
                foreach (var item in persianStr)
                {
                    try { persianStr = persianStr.Replace(item, LettersDictionaryFaToEn[item]); }
                    catch { }
                }
            }
            return persianStr;
        }

        public static string EnglishToPersianNumbers(this string persianStr)
        {
            if (persianStr.Any(x => LettersDictionaryEnToFa.Keys.Contains(x)))
            {
                foreach (var item in persianStr)
                {
                    try { persianStr = persianStr.Replace(item, LettersDictionaryEnToFa[item]); }
                    catch { }
                }
            }
            return persianStr;
        }
    }
}
