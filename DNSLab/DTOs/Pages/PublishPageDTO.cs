using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Pages
{
    public class PublishPageDTO : CreatePageDTO
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
