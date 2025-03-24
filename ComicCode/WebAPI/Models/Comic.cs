using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Comic
{
    public int ComicId { get; set; }

    public string? ComicTitle { get; set; }

    public string? ComicDescription { get; set; }

    public int? AuthorId { get; set; }

    public string? CoverImage { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<ComicGenre> ComicGenres { get; set; } = new List<ComicGenre>();
}
