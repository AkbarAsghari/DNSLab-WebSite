using DNSLab.DTOs.Tips;
using System.Text;

namespace DNSLab.Shared.SupportUs;
partial class OurSupporters
{
    List<TipDTO> result = new List<TipDTO>();
    int count = 0;

    protected override async Task OnInitializedAsync()
    {
        var Supporters = await _TransactionRepository.GetTips();

        count = Supporters.Count();

        if (count > 0)
        {

            StringBuilder sb = new StringBuilder();

            var amountUnknownSupportersCoffee = Supporters.Where(x => String.IsNullOrWhiteSpace(x.FullName));

            result.Add(new TipDTO { FullName = $"{amountUnknownSupportersCoffee.Count()} فرد ناشناس", Amount = amountUnknownSupportersCoffee.Sum(x => x.Amount), PaidDate = amountUnknownSupportersCoffee.Max(x => x.PaidDate) });
            result.AddRange(Supporters.Where(x => !String.IsNullOrWhiteSpace(x.FullName)).OrderByDescending(x => x.Amount).Select(x => new TipDTO { FullName = x.FullName, Amount = x.Amount, PaidDate = x.PaidDate }));
            if (result.Count > 4)
            {
                result[0].FullName = "💕" + result[0].FullName;
                result[1].FullName = "🥇" + result[1].FullName;
                result[2].FullName = "🥈" + result[2].FullName;
                result[3].FullName = "🥉" + result[3].FullName;
            }
        }

    }
}
