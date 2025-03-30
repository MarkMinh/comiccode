using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Author
{
    public class AuthorAddingDto
    {
        [Required(ErrorMessage = "Tên tác giả không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên tác giả tối đa 100 ký tự")]
        public string AuthorName { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Mô tả tác giả tối đa 500 ký tự")]
        public string? AuthorDescription { get; set; }
    }
}
