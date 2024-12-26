namespace DNSLab.Web.AppSettings;

public sealed class GlobalSettings
{
    public const string ApplicationName = "DNSLab";
    public const bool RightToLeft = true;
    public const bool DarkMode = false;
    public const string APIBaseAddress = "https://localhost:7046/";
}

public sealed class AuthorizeRoles
{
    public const string Admin = "Admin";
    public const string Writer = "Writer";
    public const string User = "User";
}