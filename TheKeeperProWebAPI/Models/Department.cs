using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheKeeperProWebAPI.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>();
}
