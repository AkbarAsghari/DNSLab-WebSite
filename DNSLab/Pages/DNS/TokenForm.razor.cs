using DNSLab.DTOs.DNS;

namespace DNSLab.Pages.DNS;
partial class TokenForm
{
    [Parameter] public string Title { get; set; } = String.Empty;
    [Parameter] public TokenAndDNSDTO TokenAndDNS { get; set; }
    [Parameter] public List<HostSummaryAndCheckedDTO> HostSummariesAndChecked { get; set; }


    [Parameter] public EventCallback OnValidSubmit { get; set; }


    public event Action<List<HostSummaryAndCheckedDTO>> FiltersChanged;

    private void FilterChangedBrand(HostSummaryAndCheckedDTO item, ChangeEventArgs e)
    {
        item.IsChecked = !item.IsChecked;
        if (item.IsChecked)
        {
            if (!TokenAndDNS.HostNameIds.Any(x => x == item.Id))
                TokenAndDNS.HostNameIds.Add(item.Id);
        }
        else
        {
            if (TokenAndDNS.HostNameIds.Any(x => x == item.Id))
                TokenAndDNS.HostNameIds.Remove(item.Id);
        }
        FiltersChanged?.Invoke(HostSummariesAndChecked);
    }
}
