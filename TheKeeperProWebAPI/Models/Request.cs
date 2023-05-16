using System;
using System.Collections.Generic;

namespace TheKeeperProWebAPI.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public bool GroupRequest { get; set; }

    public DateTime DesiredStartDate { get; set; }

    public DateTime? DesiredEndDate { get; set; }

    public int VisitPurpouseId { get; set; }

    public int EmployeeId { get; set; }

    public string Organisation { get; set; } = null!;

    public string? Note { get; set; }

    public int RequestStatusId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? TrueStartDate { get; set; }

    public DateTime? TrueEndDate { get; set; }

    public string? QrCode { get; set; }

    public DateTime? TrueStartDateInDivision { get; set; }

    public DateTime? TrueEndDateInDivision { get; set; }

    public virtual Employer Employee { get; set; } = null!;

    public virtual ICollection<ListOfVisiter> ListOfVisiters { get; set; } = new List<ListOfVisiter>();

    public virtual RequestStatus RequestStatus { get; set; } = null!;

    public virtual VisitPurpose VisitPurpouse { get; set; } = null!;
}
