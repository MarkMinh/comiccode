using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? AuthorName { get; set; }

    public string? AuthorDescription { get; set; }

    public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();
}
