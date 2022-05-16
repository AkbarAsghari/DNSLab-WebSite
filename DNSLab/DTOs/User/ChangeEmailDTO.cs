using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.User
{
    public class ChangeEmailDTO
    {
        [Required]
        public string Email { get; set; }
    }
}
