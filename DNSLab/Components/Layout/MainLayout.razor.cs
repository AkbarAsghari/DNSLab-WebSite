using DNSLab.DTOs.IP;
using Microsoft.AspNetCore.Http;
using MudBlazor;

namespace DNSLab.Components.Layout;

partial class MainLayout
{
    [Inject] IHttpContextAccessor httpContextAccessor { get; set; }

    private IPDTO IPDTO = new IPDTO();
    private bool _drawerOpen = true;
    private bool _isDarkMode = true;
    private MudTheme? _theme = null;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        string[] boldFontFamily = { "vazir-bold" };
        _theme = new()
        {
            Typography = new Typography()
            {
                Default = new Default()
                {
                    FontFamily = ["vazir-regular"]
                },
                H1 = new H1
                {
                    FontFamily = boldFontFamily
                },
                H2 = new H2
                {
                    FontFamily = boldFontFamily
                },
                H3 = new H3
                {
                    FontFamily = boldFontFamily
                },
                H4 = new H4
                {
                    FontFamily = boldFontFamily
                },
                H5 = new H5
                {
                    FontFamily = boldFontFamily
                },
                H6 = new H6
                {
                    FontFamily = boldFontFamily
                }
            },
            PaletteLight = _lightPalette,
            PaletteDark = _darkPalette,
            LayoutProperties = new LayoutProperties(),
        };

        IPDTO iPDTO = new IPDTO();

        try
        {
            iPDTO.IPv4 = httpContextAccessor.HttpContext.Connection?.RemoteIpAddress.ToString();
        }
        catch (Exception ex)
        {
            //If Null reference exception (Please Enable WebSocket In Feature For IIS *Akbar*)
            iPDTO.IPv4 = "Unavaible";
        }
        IPDTO = iPDTO;
    }
    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private void DarkModeToggle()
    {
        _isDarkMode = !_isDarkMode;
    }

    private readonly PaletteLight _lightPalette = new()
    {
        Black = "#110e2d",
        AppbarText = "#424242",
        AppbarBackground = "rgba(255,255,255,0.8)",
        DrawerBackground = "#ffffff",
        GrayLight = "#e8e8e8",
        GrayLighter = "#f9f9f9",
    };

    private readonly PaletteDark _darkPalette = new()
    {
        Primary = "#7e6fff",
        Surface = "#1e1e2d",
        Background = "#1a1a27",
        BackgroundGray = "#151521",
        AppbarText = "#92929f",
        AppbarBackground = "rgba(26,26,39,0.8)",
        DrawerBackground = "#1a1a27",
        ActionDefault = "#74718e",
        ActionDisabled = "#9999994d",
        ActionDisabledBackground = "#605f6d4d",
        TextPrimary = "#b2b0bf",
        TextSecondary = "#92929f",
        TextDisabled = "#ffffff33",
        DrawerIcon = "#92929f",
        DrawerText = "#92929f",
        GrayLight = "#2a2833",
        GrayLighter = "#1e1e2d",
        Info = "#4a86ff",
        Success = "#3dcb6c",
        Warning = "#ffb545",
        Error = "#ff3f5f",
        LinesDefault = "#33323e",
        TableLines = "#33323e",
        Divider = "#292838",
        OverlayLight = "#1e1e2d80",
    };

    public string DarkLightModeButtonIcon => _isDarkMode switch
    {
        true => Icons.Material.Rounded.AutoMode,
        false => Icons.Material.Outlined.DarkMode,
    };
}
