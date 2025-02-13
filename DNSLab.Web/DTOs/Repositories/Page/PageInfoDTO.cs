namespace DNSLab.Web.DTOs.Repositories.Page
{
    public class PageInfoDTO
    {
        public Guid Id { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public int VisitCount { get; set; }
        public virtual UserInfoDTO? User { get; set; }
        public DateTime CreateDate { set; get; } = DateTime.UtcNow;
        public DateTime? UpdateDate { set; get; }
    }
}
