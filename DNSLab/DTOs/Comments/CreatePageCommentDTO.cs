using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Comments
{
    public class CreatePageCommentDTO : CreateCommentDTO
    {
        public Guid PageId { get; set; }
    }
}
