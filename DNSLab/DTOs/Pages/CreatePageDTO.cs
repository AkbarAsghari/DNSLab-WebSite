using DNSLab.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Pages
{
    public class CreatePageDTO
    {
        public string URL { get; set; }
        public PageTypeEnum PageType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Keywords { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
