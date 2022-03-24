using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Pagination
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; }
    }
}
