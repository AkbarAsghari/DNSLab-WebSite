﻿using DNSLab.DTOs.IP;
using DNSLab.Interfaces.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;

namespace DNSLab.Components.Layout
{
    partial class NavMenu
    {
        [CascadingParameter] public IPDTO IPDTO { get; set; }
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }

        public async Task<bool> IsInRoleAsync(string role)
        {
            var user = (await authenticationStateTask).User;
            if (user != null)
                return user.IsInRole(role);
            return false;
        }
    }
}
