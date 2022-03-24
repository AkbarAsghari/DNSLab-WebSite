using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Pagination
{
    public class PagedListDTO<T>
    {
        public int TotalAmountPages { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
