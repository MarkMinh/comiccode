namespace WebAPI.Dto.Comic
{
    public class ComicDescriptionDto
    {
        public int ComicId { get; set; }

        public string? ComicTitle { get; set; }

        public string? ComicDescription { get; set; }

        public string? CoverImage { get; set; }

        public string? Author { get; set; }
        public List<string> Genres { get; set; }
    }
}
