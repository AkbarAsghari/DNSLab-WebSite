namespace DNSLab.Helper.Utilities
{
    public class RelativeTimeCalculator
    {
        public static string Calc(DateTime dateTime)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;
            Console.WriteLine(dateTime.ToString("HH:mm"));
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "یک ثانیه قبل" : ts.Seconds + " ثانیه قبل";

            if (delta < 2 * MINUTE)
                return "یک دقیقه قبل";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " دقیقه قبل";

            if (delta < 90 * MINUTE)
                return "یک ساعت قبل";

            if (delta < 24 * HOUR)
                return ts.Hours + " ساعت قبل";

            if (delta < 48 * HOUR)
                return "دیروز";

            if (delta < 30 * DAY)
                return ts.Days + " روز قبل";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "یک ماه قبل" : months + " ماه قبل";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "یک سال قبل" : years + " سال قبل";
            }
        }
    }
}
