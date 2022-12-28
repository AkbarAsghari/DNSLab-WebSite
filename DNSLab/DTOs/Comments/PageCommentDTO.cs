using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Comments
{
    public class PageCommentDTO : CreateCommentDTO
    {
        public Guid Id { get; set; }
        public Guid? BaseCommentId { get; set; }
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
