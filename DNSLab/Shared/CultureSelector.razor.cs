using System.Globalization;

namespace DNSLab.Shared;
partial class CultureSelector
{
    private List<BitDropDownItem> GetDropdownItems()
    {
        return new List<BitDropDownItem>()
        {
            new BitDropDownItem{ Text = "en-EN" ,Value = "en-EN"},
            new BitDropDownItem{ Text = "فارسی" ,Value = "fa-FA"}
        };
    }

    private string SelectedCulVal = CultureInfo.CurrentCulture.Name;

    public void Save()
    {
        if ((CultureInfo.CurrentCulture.Name == SelectedCulVal) ||
        (CultureInfo.CurrentCulture.Name == SelectedCulVal))
            return;

        var uri = new Uri(Nav.Uri)
          .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
        var cultureEscaped = Uri.EscapeDataString(SelectedCulVal);
        var uriEscaped = Uri.EscapeDataString(uri);

        Nav.NavigateTo(
               $"Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}",
               forceLoad: true);
    }
}
