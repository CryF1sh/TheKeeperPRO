using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheKeeperProWebAPI.Models;

public partial class Division
{
    public int DivisionId { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>();
}
