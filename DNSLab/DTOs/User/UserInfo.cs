using System.ComponentModel.DataAnnotations;

namespace DNSLab.DTOs.User
{
    public class UserInfo
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Company { get; set; }
        public bool IsEmailApproved { get; set; }
        public bool? SendEmailWhenGotTicketReply { get; set; }
        public bool? SendEmailWhenDNSIPChanged { get; set; }
    }
}
