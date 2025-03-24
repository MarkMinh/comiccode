using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Page
{
    public int PageId { get; set; }

    public int? PageNumber { get; set; }

    public int? ChapterId { get; set; }

    public string? PageImage { get; set; }

    public virtual Chapter? Chapter { get; set; }
}
