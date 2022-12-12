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

        private void onChange(string value)
        {
            BasicNoToolTipNavLinks = AllNavLinks.Where(x => x.Name.ToLower().Contains(value.ToLower())).ToList();
        }

        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private List<BitNavLinkItem> BasicNoToolTipNavLinks = new List<BitNavLinkItem>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
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

                AllNavLinks.ForEach(x => x.Links.ToList().ForEach(i => i.OnClick += ToggleNavMenu));
                AllNavLinks.Where(x => x.Links.Count() == 0).ToList().ForEach(x => x.OnClick += ToggleNavMenu);

                BasicNoToolTipNavLinks = AllNavLinks;
                await this.InvokeAsync(() => this.StateHasChanged());
            }
        }

        void ToggleNavMenu(BitNavLinkItem item)
        {
            ToggleNavMenu();
        }

        private readonly List<BitNavLinkItem> AllNavLinks = new()
        {
            new BitNavLinkItem
            {
                Name = "داشبورد",
                Url = "Dashboard",
                Key = "K001",
                IconName = BitIconName.ViewDashboard,
            }
        };

        private void AddMainMenu()
        {
            AllNavLinks.AddRange(
                new List<BitNavLinkItem> {
                    new BitNavLinkItem
                    {
                        Name = "داینامیک دی‌ان‌اس",
                        IconName = BitIconName.Dataflows,
                        Links = new List<BitNavLinkItem>
                        {
                            new BitNavLinkItem
                            {
                                Name = "هاست نِیم ها",
                                Url = "dns/mydns",
                                Key = "K301" ,
                            },
                            new BitNavLinkItem
                            {
                                Name = "توکن ها",
                                Url = "dns/mytokens",
                                Key = "K302" ,
                            }
                        }
                    },
                    new BitNavLinkItem
                    {
                        Name = "دانلود",
                        IconName = BitIconName.Download,
                        Links = new List<BitNavLinkItem>
                        {
                            new BitNavLinkItem
                            {
                                Name = "اپلیکیشن ویندوزی",
                                Url = "download/win",
                                Key = "K303" ,
                            }
                        }
                    },
                    new BitNavLinkItem
                    {
                        Name = "برای توسعه دهندگان",
                        IconName = BitIconName.AzureAPIManagement,
                        Links = new List<BitNavLinkItem>
                        {
                            new BitNavLinkItem
                            {
                                Name = "API ها",
                                Url = "api",
                                Key = "K304" ,
                            },
                            new BitNavLinkItem
                            {
                                Name = "ابزار ها",
                                Url = "tools",
                                Key = "K305" ,
                            }
                        }
                    },
                    new BitNavLinkItem
                    {
                        Name = "ارتباط با ما",
                        IconName = BitIconName.EditMail,
                        Links = new List<BitNavLinkItem>
                        {
                            new BitNavLinkItem
                            {
                                Name = "ارسال پیام",
                                Url = "Comment/MyComments",
                                Key = "K306" ,
                            },
                            new BitNavLinkItem
                            {
                                Name = "ارسال تیکت",
                                Url = "Ticket/MyTickets",
                                Key = "K307" ,
                            }
                        }
                    },
                    new BitNavLinkItem
                    {
                        Name = "اطلاعات کاربری",
                        Key = "K401",
                        IconName = BitIconName.PlayerSettings,
                        Url = "user/info",
                    },
                    new BitNavLinkItem
                    {
                        Name = "خروج",
                        Key = "K402",
                        IconName = BitIconName.PowerButton,
                        Url = "user/logout",
                    }
                });
        }

        private void AddWriterMenu()
        {
            AllNavLinks.Add(new BitNavLinkItem
            {
                Name = "پنل نویسندگان",
                IconName = BitIconName.PageEdit,
                Links = new List<BitNavLinkItem>
                {
                    new BitNavLinkItem
                    {
                        Name= "مدیریت مطالب",
                        Url = "Page/MyPages",
                        Key= "K201"
                    }
                }
            });
        }

        private void AddAdminMenu()
        {
            AllNavLinks.Add(new BitNavLinkItem
            {
                Name = "پنل ادمین",
                IconName = BitIconName.Admin,
                Links = new List<BitNavLinkItem>
                {
                    new BitNavLinkItem
                    {
                        Name = "تغییرات سایت",
                        Url = "ChangeLogs/All",
                        Key= "K101"
                    },
                    new BitNavLinkItem
                    {
                        Name = "بررسی نظرات",
                        Url = "Comment/ReviewComments",
                        Key= "K102"
                    }
                }
            });
        }
    }
}
