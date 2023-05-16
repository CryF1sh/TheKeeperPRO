using TheKeeperProWebAPI.Models;

namespace TheKeeperProWebAPI.ResponesEntities
{
    public class RequestResponse
    {
        public int requestId { get; set; }
        public bool groupRequest { get; set; }
        public DateTime desiredStartDate { get; set; }
        public Nullable<DateTime> desiredEndDate { get; set; }
        public int visitPurpouseId { get; set; }
        public string visitPurpouseName { get; set; }
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeSurname { get; set; }
        public Nullable<int> employeeDivisionId { get; set; }
        public string? employeeDivisionName { get; set; }
        public string organisation { get; set; }
        public string? note { get; set; }
        public int requestStatusId { get; set; }
        public string requestStatusName { get; set; }
        public Nullable<DateTime> startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }
        public Nullable<DateTime> trueStartDate { get; set; }
        public Nullable<DateTime> trueEndDate { get; set; }
        public string? qrCode { get; set; }
        public Nullable<DateTime> trueStartDateInDivision { get; set; }
        public Nullable<DateTime> trueEndDateInDivision { get; set; }
    }
}
