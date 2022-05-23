namespace DNSLab.Prividers
{
    public class MetadataProvider
    {
        public Dictionary<string, MetadataValue> RouteDetailMapping { get; set; } = new()
        {
            {
                "/",
                new()
                {
                    Title = "دی‌ان‌اس لب - DNSLab",
                    Description = "داینامیک دی ان اس رایگان"
                }
            },
            {
                "/user/auth",
                new()
                {
                    Title = "ورود به سیستم",
                    Description = "ورود به حساب کاربری سایت دی‌ان‌اس لب"
                }
            },
            {
                "/user/register",
                new()
                {
                    Title = "ثبت نام در سیستم",
                    Description = "ثبت نام در سایت دی‌ان‌اس لب"
                }
            }
        };
    }

    public class MetadataValue
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
