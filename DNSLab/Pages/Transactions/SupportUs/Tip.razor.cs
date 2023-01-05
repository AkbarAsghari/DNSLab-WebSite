using DNSLab.DTOs.Transactions;

namespace DNSLab.Pages.Transactions.SupportUs;
partial class Tip
{
    public TipTransactionDTO TipTransaction { get; set; } = new TipTransactionDTO { };

    string Amount { get; set; }

    public List<BitDropDownItem> AmountTypes()
    {
        return new List<BitDropDownItem>{
            new BitDropDownItem{ Value = "20000",Text = "20,000 تومان 🙂" },
            new BitDropDownItem{ Value = "50000",Text = "50,000 تومان 😃" },
            new BitDropDownItem{ Value = "100000",Text = "100,000 تومان 😍" },
            new BitDropDownItem{ Value = "0",Text = "مبلغ دلخواه 😎"},
        };
    }

    private string selectedAmountType { get; set; } = "50000";

    private async Task Go()
    {
        int amount = 0;
        if (selectedAmountType != "0")
            amount = Convert.ToInt32(selectedAmountType);
        else
            amount = Convert.ToInt32(Amount.PersianToEnglishNumbers());
        TipTransaction.Amount = amount;

        var result = await _TransactionRepository.Tip(TipTransaction);
        if (!String.IsNullOrEmpty(result))
            navigationManager.NavigateTo(result);
    }
}
