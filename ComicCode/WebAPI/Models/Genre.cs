using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public string? GenreDescription { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ComicGenre> ComicGenres { get; set; } = new List<ComicGenre>();
}
