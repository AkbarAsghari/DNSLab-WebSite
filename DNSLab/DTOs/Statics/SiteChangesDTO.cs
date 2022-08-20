using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Statics
{
    public class SiteChangesDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string InformationLink { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
    }
}
