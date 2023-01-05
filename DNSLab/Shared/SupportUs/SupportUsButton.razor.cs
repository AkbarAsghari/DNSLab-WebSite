namespace DNSLab.Shared.SupportUs;
partial class SupportUsButton
{
    int supporterCount;

    protected override async Task OnInitializedAsync()
    {
        supporterCount = (await _TransactionRepository.GetTips()).Count();
    }
}
