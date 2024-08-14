using DNSLab.DTOs.IP;
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
        [Inject] IRealTimeCommunicationRepository _RealTimeCommunicationRepository { get; set; }
        int _OnlineUsersCount = 0;

        protected override async Task OnInitializedAsync()
        {
            _RealTimeCommunicationRepository.OnUpdateUsersCount += OnlineUsersCountUpdate;
            await _RealTimeCommunicationRepository.CheckOnlineUsersCount(IPDTO.IPv4);
        }

        private void OnlineUsersCountUpdate(int count)
        {
            _OnlineUsersCount = count;
            this.InvokeAsync(() => StateHasChanged());
        }

        public async Task<bool> IsInRoleAsync(string role)
        {
            var user = (await authenticationStateTask).User;
            if (user != null)
                return user.IsInRole(role);
            return false;
        }
    }
}
