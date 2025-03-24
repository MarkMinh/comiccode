using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ComicGenre
{
    public int ComicGenreId { get; set; }

    public int? ComicId { get; set; }

    public int? GenreId { get; set; }

    public virtual Comic? Comic { get; set; }

    public virtual Genre? Genre { get; set; }
}
