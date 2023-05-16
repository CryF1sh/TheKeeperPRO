using System;
using System.Collections.Generic;

namespace TheKeeperProWebAPI.Models;

public partial class RequestStatus
{
    public int RequestStatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
