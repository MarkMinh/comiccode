namespace WebAPI.Dto.Comic
{
    public class ComicListDto
    {
        public int ComicId { get; set; }

        public string? ComicTitle { get; set; }

        public string? CoverImage { get; set; }

        public List<string> Genres { get; set; }
    }
}
