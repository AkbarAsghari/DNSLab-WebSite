using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Pages
{
    public class ChangeLogDTO
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string? InformationLink { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
