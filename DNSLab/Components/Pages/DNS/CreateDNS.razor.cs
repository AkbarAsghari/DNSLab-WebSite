﻿using DNSLab.DTOs.DNS;

namespace DNSLab.Components.Pages.DNS;
partial class CreateDNS
{
    private HostNameDTO hostName = new HostNameDTO() { RecordType = 1 };

    public async Task Create()
    {
        hostName.Domain = "dnslab.link";

        if (await dnsRepository.CreateHostName(hostName))
        {
            _navManager.NavigateTo("dns/mydns");
            Snackbar.Add(localizer["HostNameCreated"], MudBlazor.Severity.Success);
        }
    }
}
