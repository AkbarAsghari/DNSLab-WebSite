using System;

namespace DNSLab.Pages.DNS;

partial class Integration
{
    private int _ActivePanelIndex;

    [Inject] NavigationManager NavigationManager { get; set; }
    int ActivePanelIndex
    {
        get
        {
            return _ActivePanelIndex;
        }
        set
        {
            _ActivePanelIndex = value;
            string route = String.Empty;
            switch (value)
            {
                case 0:
                default:
                    route = "dns/integration/routers";
                    break;
                case 1:
                    route = "dns/integration/mikrotik";
                    break;
                case 2:
                    route = "dns/integration/scripts";
                    break;
                case 3:
                    route = "dns/integration/software";
                    break;
            }

            NavigationManager.NavigateTo(route, false);
        }
    }
    [Parameter] public string With { get; set; }

    protected override void OnInitialized()
    {
        if (String.IsNullOrEmpty(With))
        {
            ActivePanelIndex = 0;
            return;
        }

        switch (With.ToLower())
        {
            case "routers":
                ActivePanelIndex = 0;
                break;
            case "mikrotik":
                ActivePanelIndex = 1;
                break;
            case "scripts":
                ActivePanelIndex = 2;
                break;
            case "software":
                ActivePanelIndex = 3;
                break;
            default:
                ActivePanelIndex = 0;
                break;
        }
    }

}
