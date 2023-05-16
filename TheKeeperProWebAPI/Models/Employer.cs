using System;
using System.Collections.Generic;

namespace TheKeeperProWebAPI.Models;

public partial class Employer
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public int? DivisionId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Division? Division { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
