using DNSLab.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DNSLab.Helper.Exceptions
{
    public class ExceptionsAndNotificationLevel
    {
        public string ExceptionStr { get; set; }
        public Enums.ToastLevel Level { get; set; }
        public string NormalMessage { get; set; }
    }

    public sealed class ApplicationExceptions
    {
        private readonly IStringLocalizer<Resource> _localizer;

        public ApplicationExceptions(IStringLocalizer<Resource> localizer)
        {
            _localizer = localizer;
        }

        private static List<ExceptionsAndNotificationLevel> _Exceptions;
        public List<ExceptionsAndNotificationLevel> GetExceptions()
        {
            if (_Exceptions == null)
            {
                _Exceptions = new List<ExceptionsAndNotificationLevel>{
                    new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Username_Is_Duplicate",
                         Level = Enums.ToastLevel.Error,
                         NormalMessage = _localizer["Username_Is_Duplicate"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Email_Is_Duplicate",
                         Level = Enums.ToastLevel.Error,
                         NormalMessage = _localizer["Email_Is_Duplicate"] 
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Mobile_Is_Duplicate",
                         Level = Enums.ToastLevel.Error,
                         NormalMessage = _localizer["Mobile_Is_Duplicate"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "User_Not_Found",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["User_Not_Found"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Email_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Email_Is_Not_Valid"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Username_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Username_Is_Not_Valid"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Username_Or_Password_Is_Wrong",
                         Level = Enums.ToastLevel.Error,
                         NormalMessage = _localizer["Username_Or_Password_Is_Wrong"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "HostName_Is_Duplicate",
                         Level = Enums.ToastLevel.Error,
                         NormalMessage = _localizer["HostName_Is_Duplicate"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "HostName_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["HostName_Is_Not_Valid"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "IPv4Address_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["IPv4Address_Is_Not_Valid"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "IPv6Address_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["IPv6Address_Is_Not_Valid"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "HostNameAlias_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["HostNameAlias_Is_Not_Valid"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Token_Not_Found",
                         Level = Enums.ToastLevel.Error,
                         NormalMessage = _localizer["Token_Not_Found"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Token_Expired",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Token_Expired"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Token_Used_Before",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Token_Used_Before"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "TokenName_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["TokenName_Is_Not_Valid"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "No_HostName_Selected",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["No_HostName_Selected"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Password_Less_Than_8_Character",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Password_Less_Than_8_Character"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Comment_Not_Found",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Comment_Not_Found"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Comment_Text_Is_Empty",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Comment_Text_Is_Empty"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Mobile_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Mobile_Is_Not_Valid"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Old_Password_Is_Wrong",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Old_Password_Is_Wrong"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Can_Not_Use_Private_Network_IP_Addresses",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Can_Not_Use_Private_Network_IP_Addresses"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Only_Letter_And_Dash_Accepted_In_URL",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Only_Letter_And_Dash_Accepted_In_URL"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "ChangeLog_Not_Found",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["ChangeLog_Not_Found"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "ChangeLog_ReleaseDate_Range_MustBe_From_7Days_Ago_ToNow",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["ChangeLog_ReleaseDate_Range_MustBe_From_7Days_Ago_ToNow"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "ChangeLog_Must_Have_InformationLink_When_Contain_Description",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["ChangeLog_Must_Have_InformationLink_When_Contain_Description"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "ChangeLog_Title_Is_Not_Valid",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["ChangeLog_Title_Is_Not_Valid"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Page_URL_Is_Duplicate",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Page_URL_Is_Duplicate"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Page_Body_Can_Not_Be_Empty",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Page_Body_Can_Not_Be_Empty"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Page_Description_Can_Not_Be_Empty",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Page_Description_Can_Not_Be_Empty"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Page_Keywords_Must_Have_At_Least_One_Item",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Page_Keywords_Must_Have_At_Least_One_Item"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Page_Not_Found",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Page_Not_Found"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Page_Title_Can_Not_Be_Empty",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Page_Title_Can_Not_Be_Empty"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Page_Type_Not_Found",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Page_Type_Not_Found"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Page_URL_Can_Not_Be_Empty",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Page_URL_Can_Not_Be_Empty"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Ticket_Not_Found",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Ticket_Not_Found"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Ticket_Text_Is_Empty",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Ticket_Text_Is_Empty"]
                    }
                    ,new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Ticket_Title_Is_Empty",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Ticket_Title_Is_Empty"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Transaction_Amount_Must_Be_Gear_Than_5000",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Transaction_Amount_Must_Be_Gear_Than_5000"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Transaction_Not_Found",
                         Level = Enums.ToastLevel.Warning,
                         NormalMessage = _localizer["Transaction_Not_Found"]
                    },new ExceptionsAndNotificationLevel()
                    {
                         ExceptionStr = "Paymanet_Exception",
                         Level = Enums.ToastLevel.Error,
                         NormalMessage = _localizer["Paymanet_Exception"]
                    }
                };
            }
            return _Exceptions;
        }

    }
}
