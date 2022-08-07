using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Comments
{
    public class FullCommentDTO : CommentDTO
    {
        public string? FullName { get; set; } = String.Empty;
        public int RepliesCount { get; set; } = 0;
    }
}
