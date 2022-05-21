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
                    Title = "DNSLab",
                    Description = "Free dynamic dns"
                }
            },
            {
                "/user/auth",
                new()
                {
                    Title = "ورود",
                    Description = "ورود به حساب کاربری"
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
