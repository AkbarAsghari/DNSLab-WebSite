﻿using DNSLab.DTOs.User;

namespace DNSLab.Components.Pages.User;
partial class ForgetPassword
{
    private ForgetPasswordDTO forgetPassword = new ForgetPasswordDTO();
    private bool isForgetPasswordLinkSent = false;

    private async Task SendForgetPasswordLink()
    {
        isForgetPasswordLinkSent = await accountReository.ForgetPassword(forgetPassword);
    }
}
