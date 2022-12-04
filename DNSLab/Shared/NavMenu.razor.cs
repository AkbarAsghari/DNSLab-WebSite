using Bit.BlazorUI;
using DNSLab.DTOs.IP;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace DNSLab.Shared
{
    partial class NavMenu
    {
        [CascadingParameter] public IPDTO IPDTO { get; set; }

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

        private readonly List<BitNavLinkItem> AllNavLinks = new()
        {
            new BitNavLinkItem
            {
                Name = "Home",
                Url = "/",
                Title = "",
                Key = "Key1",
                CollapseAriaLabel = "Collapse Home section",
                ExpandAriaLabel = "Expand Home section",
                IconName = BitIconName.Home,
            },
            new BitNavLinkItem
            {
                Name = "داینامیک دی‌ان‌اس",
                Key = "Key3" ,
                IconName = BitIconName.Dataflows,
                Links = new List<BitNavLinkItem>
                {
                    new BitNavLinkItem
                    {
                        Name = "هاست نِیم ها",
                        Url = "dns/mydns",
                        Key = "Key4" ,
                    },
                    new BitNavLinkItem
                    {
                        Name = "توکن ها",
                        Url = "dns/mytokens",
                        Key = "Key5" ,
                    }
                }
            },
            new BitNavLinkItem
            {
                Name = "دانلود",
                Key = "Key6" ,
                IconName = BitIconName.Download,
                Links = new List<BitNavLinkItem>
                {
                    new BitNavLinkItem
                    {
                        Name = "اپلیکیشن ویندوزی",
                        Url = "download/win",
                        Key = "Key7" ,
                    }
                }
            },
            new BitNavLinkItem
            {
                Name = "ارتباط با ما",
                Key = "Key8" ,
                IconName = BitIconName.Contact,
                Links = new List<BitNavLinkItem>
                {
                    new BitNavLinkItem
                    {
                        Name = "ارسال پیام",
                        Url = "Comment/MyComments",
                        Key = "Key9" ,
                    },
                    new BitNavLinkItem
                    {
                        Name = "ارسال تیکت",
                        Url = "Ticket/MyTickets",
                        Key = "Key10" ,
                    }
                }
            },
            new BitNavLinkItem
            {
                Name = "خروج",
                Key = "Key12",
                IconName = BitIconName.PowerButton,
                Url="user/logout",
            }
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                BasicNoToolTipNavLinks = AllNavLinks;
                await this.InvokeAsync(() => this.StateHasChanged());
            }
        }
    }
}
