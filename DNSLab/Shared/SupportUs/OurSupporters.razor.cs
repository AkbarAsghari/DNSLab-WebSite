using DNSLab.DTOs.Tips;
using MD.PersianDateTime.Standard;
using System.Text;

namespace DNSLab.Shared.SupportUs;
partial class OurSupporters
{
    TipsInformationDTO _TipsInformation = new TipsInformationDTO();

    protected override async Task OnInitializedAsync()
    {
        _TipsInformation = await _TransactionRepository.GetTipsInformation();
    }
}
