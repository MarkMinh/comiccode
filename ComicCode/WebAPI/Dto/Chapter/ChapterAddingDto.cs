using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Chapter
{
    public class ChapterAddingDto
    {
        [Required]
        public int? ComicId { get; set; }

        [Required]
        public int? ChapterNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string? ChapterTitle { get; set; }
    }
}
