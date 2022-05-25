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
            },
            {
                "/api",
                new()
                {
                    Title = "API های سایت",
                    Description = "API - برای توسعه دهندگان"
                }
            },
            {
                "/Comment/Create",
                new()
                {
                    Title = "ارسال دیدگاه",
                    Description = "ارسال دیدگاه برای سایت dnslab"
                }
            },
            {
                "/Comment/Edit/{CommentId:guid}",
                new()
                {
                    Title = "ویرایش دیدگاه ارسال شده",
                    Description = "ویرایش دیدگاه ارسال شده به سایت dnslab"
                }
            },
            {
                "/Comment/MyComments",
                new()
                {
                    Title = "دیدگاه‌های من",
                    Description = "دیدگاه‌های من"
                }
            },
            {
                "/legal/tos",
                new()
                {
                    Title = "قوانین و شرایط استفاده",
                    Description = "قوانین و شرایط استفاده از سایت dnslab"
                }
            },
            {
                "/dns/create",
                new()
                {
                    Title = "ایجاد هاست‌نیم جدید",
                    Description = "صفحه ایجاد هاست‌نیم یا دی‌ان‌اس جدید"
                }
            },
            {
                "/dns/createtoken",
                new()
                {
                    Title = "ایجاد توکن جدید",
                    Description = "صفحه ایحاد توکن جدید"
                }
            },
            {
                "/DNS/Histories",
                new()
                {
                    Title = "تاریخچه تغییرات",
                    Description = "تاریخچه تغییرات آی‌پی و هاست‌نیم های شما"
                }
            },
            {
                "/dns/edit/{HostNameId:guid}",
                new()
                {
                    Title = "ویرایش هاست‌نیم",
                    Description = "ویرایش هاست‌نیم ایجاد شده"
                }
            },
            {
                "/dns/token/edit/{TokenId:guid}",
                new()
                {
                    Title = "ویرایش توکن",
                    Description = "ویرایش توکن ایجاد شده"
                }
            },
            {
                "/dns/mydns",
                new()
                {
                    Title = "هاست‌نیم ها",
                    Description = "دی‌ان‌اس یا هاست نیم های من"
                }
            },
            {
                "/dns/mytokens",
                new()
                {
                    Title = "توکن ها",
                    Description = "توکن های ثبت شما من"
                }
            },
            {
                "/download/win",
                new()
                {
                    Title = "صفحه دانلود",
                    Description = "دانلود برنامه های DNSLab"
                }
            },
            {
                "/404",
                new()
                {
                    Title = "صفحه مورد نظر یافت نشد",
                    Description = "صفحه مورد نظر یافت نشد"
                }
            },
            {
                "/500",
                new()
                {
                    Title = "خطای داخلی سرور رخ داده",
                    Description = "خطای داخلی سرور رخ داده"
                }
            },
            {
                "/503",
                new()
                {
                    Title = "سرویس در دسترس نیست",
                    Description = "سرویس در دسترس نیست"
                }
            },
            {
                "/Support",
                new()
                {
                    Title = "پشتیبانی",
                    Description = "صفحه پشتیبانی و راهنمای سایت DNSLab"
                }
            },
            {
                "/tools/dnslookup",
                new()
                {
                    Title = "جستجوی دی‌ان‌اس",
                    Description = "جستجو و دریافت آی‌پی بر اساس دی‌ان‌اس"
                }
            },
            {
                "/tools/ping",
                new()
                {
                    Title = "پینگ",
                    Description = "بررسی پینگ بر اساس آی‌پی یا ‌آدرس"
                }
            },
            {
                "/tools/port",
                new()
                {
                    Title = "بررسی پورت",
                    Description = "بررسی باز بودن پورت آی‌پی یا آدرس"
                }
            },
            {
                "/tools/reverselookup",
                new()
                {
                    Title = "جستجوی معکوس",
                    Description = "جستجو بر اساس آی‌پی و بدست آوردن آدرس DNS (دی‌ان‌اس)"
                }
            },
            {
                "/tools",
                new()
                {
                    Title = "ابزار",
                    Description = "ابزار های سایت DNSLab"
                }
            },
            {
                "/user/ChangePassword",
                new()
                {
                    Title = "تغییر رمز عبور",
                    Description = "تغییر رمز عبور در سایت"
                }
            },
            {
                "/User/ConfirmEmail/{Token}",
                new()
                {
                    Title = "تایید ایمیل",
                    Description = "تایید ایمیل حساب کاربری در سایت dnslab"
                }
            },
            {
                "/user/ForgetPassword",
                new()
                {
                    Title = "فراموشی رمز عبور",
                    Description = "فراموشی رمز عبور ، رمز عبور خود را فراموش کرده ام"
                }
            },
            {
                "/user/info",
                new()
                {
                    Title = "اطلاعات حساب کاربری",
                    Description = "اطلاعات حساب کاربری و اطلاعات ورود و نام کاربری و ..."
                }
            },
            {
                "/user/logout",
                new()
                {
                    Title = "خروج",
                    Description = "خروج"
                }
            },
            {
                "/user/ResetPassword/{Token}",
                new()
                {
                    Title = "بازیابی رمز عبور",
                    Description = "بازیابی رمز عبور و انتخاب رمز جدید"
                }
            }
        };
    }

    public class MetadataValue
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Keywords { get; set; }
    }
}
