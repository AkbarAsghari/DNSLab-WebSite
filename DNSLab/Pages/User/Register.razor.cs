using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class Register
{
    private RegisterUserDTO registerUser = new RegisterUserDTO();

    bool isApprovedTOS = false;

    private async Task CreateUser()
    {
        if (isApprovedTOS)
        {
            var userToken = await accountReository.Register(registerUser);
            if (!String.IsNullOrEmpty(userToken))
            {
                await authService.Login(userToken);
                navigationManager.NavigateTo("Dashboard");
            }
        }
        else
        {
            toastService.ShowToast("لطفا تیک قوانین و مقررات بزنید", ToastLevel.Info);
        }
    }
}
