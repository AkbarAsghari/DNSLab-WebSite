using System.Text.RegularExpressions;

namespace DNSLab.Helper.Utilities
{
    public static class RegexTools
    {
        public static IEnumerable<string> ExtractUrlsFromString(string input)
        {
            var matches = new Regex(@"(ftp:\/\/|www\.|https?:\/\/){1}[a-zA-Z0-9u00a1-\uffff0-]{2,}\.[a-zA-Z0-9u00a1-\uffff0-]{2,}(\S*)").Matches(input);

            return matches.Select(x => x.Value);
        }
    }
}
