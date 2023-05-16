using System;
using System.Collections.Generic;

namespace TheKeeperProWebAPI.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int UserId { get; set; }

    public string Message1 { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
