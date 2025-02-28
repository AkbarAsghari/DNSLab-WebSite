namespace DNSLab.Web.Helpers
{
    public sealed class AllRoutes
    {
        public const string Home = "/";

        public const string ItIsUnderDevelopment = "/itisunderdevelopment";

        public const string Dashboard = "/dashboard";
        public const string About = "/about";

        public const string AllZones = "/zones/all";
        public const string AllRecords = "/records";

        public const string AllTools = "/tools/all";
        public const string DNSLookup = "/tools/dnslookup";
        public const string Ping = "/tools/ping";
        public const string ReverseLookup = "/tools/reverselookup";
        public const string PortChecker = "/tools/portchecker";

        public const string DDNSHostNames = "/ddns/hostnames";
        public const string UpdateTokens = "/ddns/updatetokens";
        public const string RouterSettings = "/ddns/routersettings";
        public const string MikroTikSettings = "/ddns/mikrotiksettings";

        public const string MyWallet = "/wallet/my";
        public const string WalletTransactions = "/wallet/transactions";

        public const string AllTickets = "/tickets/all";
        public const string MyTickets = "/tickets/my";
        public const string TicketMessages = "/tickets/messages";

        public const string MyAccount = "/accounts/my";
        public const string Login = "/accounts/login";
        public const string Logout = "/accounts/logout";
        public const string RegisterAccount = "/accounts/register";
        public const string AllAcounts = "/accounts/all";
        public const string ForgetPassword = "/accounts/forget";
        public const string ResetPassword = "/accounts/reset";
        public const string ConfirmEmail = "/accounts/confirm-email";

        public const string AllPages = "/pages/all";
        public const string Page = "/pages/page";
        public const string Blog = "/blog";
        public const string AllBlogs = "/blog/all";

        public const string CallBackPayment = "payment/callback";
        public const string Payments = "/payment/payments";
        public const string AllPayments = "/payment/allpayments";

        public const string TodayChangesDDNS = "/admin/todaychangesddns";
    }
}
