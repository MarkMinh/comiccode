using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Comic
{
    public class ComicAddingDto
    {
        [Required(ErrorMessage = "Comic title is required")]
        [StringLength(200, ErrorMessage = "Title can't be longer than 200 characters")]
        public string? ComicTitle { get; set; }

        [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters")]
        public string? ComicDescription { get; set; }

        [Required(ErrorMessage = "AuthorId is required")]
        public int? AuthorId { get; set; }
        public IFormFile? CoverImage { get; set; }
    }
}
