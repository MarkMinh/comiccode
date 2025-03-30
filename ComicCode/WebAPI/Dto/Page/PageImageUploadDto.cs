using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Page
{
    public class PageImageUploadDto
    {
        [Required]
        public int PageNumber { get; set; }

        [Required]
        public IFormFile PageImage { get; set; }
    }
}
