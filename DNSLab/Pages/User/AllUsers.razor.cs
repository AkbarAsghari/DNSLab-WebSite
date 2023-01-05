using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class AllUsers
{
    IEnumerable<UserDTO> allUsers;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            allUsers = await accountReository.GetAll();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }
}
