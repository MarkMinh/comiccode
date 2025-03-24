using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Chapter
{
    public int ChapterId { get; set; }

    public int? ChapterNumber { get; set; }

    public int? ComicId { get; set; }

    public string? ChapterTitle { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Comic? Comic { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();
}
