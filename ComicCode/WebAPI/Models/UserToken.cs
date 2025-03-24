using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class UserToken
{
    public int UserTokenId { get; set; }

    public int UserId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExpiryTime { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual User User { get; set; } = null!;
}
