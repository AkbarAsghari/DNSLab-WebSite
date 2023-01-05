using DNSLab.DTOs.Tips;
using System.Text;

namespace DNSLab.Shared.SupportUs;
partial class OurSupporters
{
    List<TipDTO> result = new List<TipDTO>();

    protected override async Task OnInitializedAsync()
    {
        var Supporters = await _TransactionRepository.GetTips();

        if (Supporters.Count() > 0)
        {
            StringBuilder sb = new StringBuilder();

            var amountUnknownSupportersCoffee = Supporters.Where(x => String.IsNullOrWhiteSpace(x.FullName));

            result.Add(new TipDTO { FullName = $"{amountUnknownSupportersCoffee.Count()} فرد ناشناس", Amount = amountUnknownSupportersCoffee.Sum(x => x.Amount) });
            result.AddRange(Supporters.Where(x => !String.IsNullOrWhiteSpace(x.FullName)).OrderByDescending(x => x.PaidDate).Select(x => new TipDTO { FullName = x.FullName, Amount = x.Amount }));
        }

    }
}
