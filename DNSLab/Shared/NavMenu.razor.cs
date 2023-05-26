using Bit.BlazorUI;
using DNSLab.DTOs.IP;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;

namespace DNSLab.Shared
{
    partial class NavMenu
    {
        [CascadingParameter] public IPDTO IPDTO { get; set; }
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }

        public class DNSLabMenuItem
        {
            public string Text { get; set; } = string.Empty;
            public string Url { get; set; } = string.Empty;
            public BitIconName Icon { get; set; }
            public List<DNSLabMenuItem> Links { get; set; } = new();
        }

        private void onClearSearch()
        {
            searchedText = String.Empty;
        }

        private void onChange(string value)
        {
            searchedText = value;
        }
        private string searchedText = String.Empty;

        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
        private void ToggleNavMenu(bool toggle)
        {
            if (toggle)
            {
                ToggleNavMenu();
            }
        }


        protected override async Task OnInitializedAsync()
        {
            var user = (await authenticationStateTask).User;

            if (user != null)
            {
                if (user.IsInRole("Admin"))
                    AddAdminMenu();

                if (user.IsInRole("Admin") || user.IsInRole("Writer"))
                    AddWriterMenu();
            }

            AddMainMenu();

        }

        List<DNSLabMenuItem> AllNavLinks = new()
        {
            new DNSLabMenuItem
            {
                Text = "خانه",
                Url = "/",
                Icon = BitIconName.Home
            },
            new DNSLabMenuItem
            {
                Text = "داشبورد",
                Url = "Dashboard",
                Icon = BitIconName.ViewDashboard,
            }
        };

        private void AddMainMenu()
        {
            AllNavLinks.AddRange(
                new List<DNSLabMenuItem> {
                    new DNSLabMenuItem
                    {
                        Text = "داینامیک دی‌ان‌اس",
                        Icon = BitIconName.Dataflows,
                        Links = new List<DNSLabMenuItem>
                        {
                            new DNSLabMenuItem
                            {
                                Text = "هاست نِیم ها",
                                Url = "dns/mydns"
                            },
                            new DNSLabMenuItem
                            {
                                Text = "توکن ها",
                                Url = "dns/mytokens"
                            }
                        }
                    },
                    new DNSLabMenuItem
                    {
                        Text = "گزارش ها",
                        Icon = BitIconName.BarChartVertical,
                        Links = new List<DNSLabMenuItem>
                        {
                            new DNSLabMenuItem
                            {
                                Text = "گزارش تغییرات هاست نِیم ها",
                                Url = "Report/DNSHistories"
                            },
                            new DNSLabMenuItem
                            {
                                Text = "جدول تغییرات آی پی",
                                Url = "Report/IPChangesChart"
                            }
                        }
                    },
                    new DNSLabMenuItem
                    {
                        Text = "مرکز آموزش",
                        Icon= BitIconName.Source,
                        Links = new List<DNSLabMenuItem>
                        {
                            new DNSLabMenuItem
                            {
                                Text = "راهنمای سایت",
                                Url = "support"
                            },
                            new DNSLabMenuItem
                            {
                                Text = "آموزش ها",
                                Url = "articles"
                            }
                        }
                    },
                    new DNSLabMenuItem
                    {
                        Text = "دانلود",
                        Icon = BitIconName.Download,
                        Links = new List<DNSLabMenuItem>
                        {
                            new DNSLabMenuItem
                            {
                                Text = "اپلیکیشن ویندوزی",
                                Url = "download/win"
                            }
                        }
                    },
                    new DNSLabMenuItem
                    {
                        Text = "برای توسعه دهندگان",
                        Icon = BitIconName.AzureAPIManagement,
                        Links = new List<DNSLabMenuItem>
                        {
                            new DNSLabMenuItem
                            {
                                Text = "API ها",
                                Url = "api"
                            }
                        }
                    },
                    new DNSLabMenuItem
                    {
                        Text = "ابزار ها",
                        Icon = BitIconName.DeveloperTools,
                        Links = new List<DNSLabMenuItem>
                        {
                            new DNSLabMenuItem
                            {
                                Text = "Ping",
                                Url = "tools/ping"
                            },
                            new DNSLabMenuItem
                            {
                                Text = "Port Checker",
                                Url = "tools/port"
                            },
                            new DNSLabMenuItem
                            {
                                Text = "DNS Lookup",
                                Url = "tools/dnslookup"
                            },
                            new DNSLabMenuItem
                            {
                                Text = "Reverse Lookup",
                                Url = "tools/reverselookup"
                            }
                        }
                    },
                    new DNSLabMenuItem
                    {
                        Text = "پشتیبانی",
                        Icon = BitIconName.HeadsetSolid,
                        Links = new List<DNSLabMenuItem>
                        {
                            new DNSLabMenuItem
                            {
                                Text = "ارسال تیکت",
                                Url = "Ticket/MyTickets"
                            }
                        }
                    },
                    new DNSLabMenuItem
                    {
                        Text = "ناحیه کاربری",
                        Icon = BitIconName.Settings,
                        Links = new List<DNSLabMenuItem>
                        {
                            new DNSLabMenuItem
                            {
                                Text = "پروفایل عمومی",
                                Url = "settings/profile"
                            },
                            new DNSLabMenuItem {
                                Text = "حساب کاربری",
                                Url = "settings/admin"
                            },
                            new DNSLabMenuItem {
                                Text = "رمز عبور و احراز هویت",
                                Url = "settings/security"
                            },
                            new DNSLabMenuItem {
                                Text = "تنظیمات",
                                Url = "settings/you"
                            }
                        }
                    },
                    new DNSLabMenuItem
                    {
                        Text = "خروج",
                        Icon = BitIconName.PowerButton,
                        Url = "user/logout"
                    }
                });
        }

        private void AddWriterMenu()
        {
            AllNavLinks.Add(new DNSLabMenuItem
            {
                Text = "پنل نویسندگان",
                Icon = BitIconName.PageEdit,
                Links = new List<DNSLabMenuItem>
                {
                    new DNSLabMenuItem
                    {
                        Text= "مدیریت مطالب",
                        Url = "Page/MyPages"
                    },
                    new DNSLabMenuItem
                    {
                        Text= "دیدگاه‌ها",
                        Url = "Comment/Pages"
                    }
                }
            });
        }

        private void AddAdminMenu()
        {
            AllNavLinks.Add(new DNSLabMenuItem
            {
                Text = "پنل ادمین",
                Icon = BitIconName.Admin,
                Links = new List<DNSLabMenuItem>
                {
                    new DNSLabMenuItem
                    {
                        Text = "تغییرات سایت",
                        Url = "ChangeLogs/All"
                    },
                    new DNSLabMenuItem
                    {
                        Text = "همه هاست نِیم ها",
                        Url = "DNS/AllHostNames"
                    }
                }
            });
        }
    }
}
