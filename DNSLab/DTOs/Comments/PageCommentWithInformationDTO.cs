using DNSLab.DTOs.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Comments
{
    public class PageCommentWithInformationDTO
    {
        public PageSummaryDTO Page { get; set; }
        public PageCommentDTO Comment { get; set; }
        public PageCommentDTO? ReplyOf { get; set; }
    }
}
