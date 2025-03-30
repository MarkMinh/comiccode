namespace WebAPI.Dto.Page
{
    public class PageDto
    {
        public int PageId { get; set; }
        public int? PageNumber { get; set; }
        public int? ChapterId { get; set; }
        public string? PageImage { get; set; }
    }
}
