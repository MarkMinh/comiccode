using WebAPI.Dto.Page;

namespace WebAPI.Dto.Chapter
{
    public class ChapterDto
    {
        public int ChapterId { get; set; }
        public int? ComicId { get; set; }
        public int? ChapterNumber { get; set; }
        public string? ChapterTitle { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? IsActive { get; set; }

        public List<PageDto> Pages { get; set; }
    }
}
