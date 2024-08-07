﻿using DNSLab.DTOs.User;

namespace DNSLab.Components.Pages.User;
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
            Snackbar.Add("لطفا تیک قوانین و مقررات بزنید", MudBlazor.Severity.Info);
        }
    }
}
