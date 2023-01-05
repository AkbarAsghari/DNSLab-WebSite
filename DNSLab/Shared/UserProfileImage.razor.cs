using Jdenticon;

namespace DNSLab.Shared;
partial class UserProfileImage
{
    [Parameter] public int Size { get; set; }
    [Parameter] public string UserId { get; set; } = String.Empty;

    private string GenerateProfilePhoto(string userId)
    {
        return Identicon.FromValue(userId, size: Size == 0 ? 100 : Size).ToSvg();
    }
}
