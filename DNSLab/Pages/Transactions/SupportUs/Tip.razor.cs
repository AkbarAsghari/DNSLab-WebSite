using DNSLab.DTOs.Transactions;

namespace DNSLab.Pages.Transactions.SupportUs;
partial class Tip
{
    public TipTransactionDTO TipTransaction { get; set; } = new TipTransactionDTO { };

    string Amount { get; set; }

    public Dictionary<string, string> AmountTypes = new Dictionary<string, string>{
            { "20000" , "20,000 تومان 🙂" },
            { "50000", "50,000 تومان 😃" },
            { "100000", "100,000 تومان 😍" },
            { "0", "مبلغ دلخواه 😎"},
        };

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
