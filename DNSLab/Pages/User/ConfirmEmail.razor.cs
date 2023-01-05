namespace DNSLab.Pages.User;
partial class ConfirmEmail
{
    [Parameter] public string Token { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (await accountReository.ConfirmEmailWithToken(Token))
            navigation.NavigateTo("/");
    }
}
