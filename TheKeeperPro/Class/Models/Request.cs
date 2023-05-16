using System;
using System.Collections.Generic;

namespace TheKeeperPro.Class
{
    public partial class Request
    {
        public int requestId { get; set; }

        public bool groupRequest { get; set; }

        public DateTime desiredStartDate { get; set; }

        public DateTime? desiredEndDate { get; set; }

        public int visitPurpouseId { get; set; }

        public int employeeId { get; set; }

        public string organisation { get; set; }

        public string note { get; set; }

        public int requestStatusId { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public DateTime? trueStartDate { get; set; }

        public DateTime? trueEndDate { get; set; }

        public string qrCode { get; set; }

        public DateTime? trueStartDateInDivision { get; set; }

        public DateTime? trueEndDateInDivision { get; set; }

        public virtual Employer employee { get; set; }

        public virtual ICollection<ListOfVisiter> listOfVisiters { get; set; } = new List<ListOfVisiter>();

        public virtual RequestStatus requestStatus { get; set; }

        public virtual VisitPurpose visitPurpouse { get; set; }
    }

}

