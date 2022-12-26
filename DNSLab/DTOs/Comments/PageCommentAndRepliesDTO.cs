using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Comments
{
    public class PageCommentAndRepliesDTO
    {
        public PageCommentDTO Comment { get; set; }
        public IEnumerable<PageCommentDTO> Replies { get; set; }
    }
}
