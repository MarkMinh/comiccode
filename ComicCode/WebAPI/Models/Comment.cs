using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? UserId { get; set; }

    public string? CommentContent { get; set; }

    public int? CommentParentId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsActive { get; set; }

    public int? ChapterId { get; set; }

    public virtual Chapter? Chapter { get; set; }

    public virtual User? User { get; set; }
}
