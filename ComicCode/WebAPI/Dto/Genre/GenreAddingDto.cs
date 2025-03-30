using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Genre
{
    public class GenreAddingDto
    {
        [Required(ErrorMessage = "Genre name is required.")]
        [MaxLength(100, ErrorMessage = "Genre name cannot exceed 100 characters.")]
        public string? GenreName { get; set; }

        [MaxLength(500, ErrorMessage = "Genre description cannot exceed 500 characters.")]
        public string? GenreDescription { get; set; }
    }
}
