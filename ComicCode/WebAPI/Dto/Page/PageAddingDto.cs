using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Page
{
    public class PageAddingDto
    {
        [Required]
        public int ChapterId { get; set; }

        [Required]
        public List<IFormFile> PageImages { get; set; }
    }
}
