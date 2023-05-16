using System;
namespace TheKeeperPro.Class
{
    public class RequestToView
    {
        public int requestId { get; set; }
        public bool groupRequest { get; set; }
        public string typeOfRequest
        {
            get 
            {
                return groupRequest ? "Групповая" : "Одиночная";
            } 
        }
        public DateTime desiredStartDate { get; set; }
        public Nullable<DateTime> desiredEndDate { get; set; }
        public int visitPurpouseId { get; set; }
        public string visitPurpouseName { get; set; }
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeSurname { get; set; }
        public Nullable<int> employeeDivisionId { get; set; }
        public string employeeDivisionName { get; set; }
        public string organisation { get; set; }
        public string note { get; set; }
        public int requestStatusId { get; set; }
        public string requestStatusName { get; set; }
        public Nullable<DateTime> startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }
        public Nullable<DateTime> trueStartDate { get; set; }
        public Nullable<DateTime> trueEndDate { get; set; }
        public string qrCode { get; set; }
        public Nullable<DateTime> trueStartDateInDivision { get; set; }
        public Nullable<DateTime> trueEndDateInDivision { get; set; }
        public int? requestCreaterId { get; set; }
        public string requestCreaterName { get; set; }
        public string requestCreaterSurname { get; set; }
        public string requestCreaterPatronymic { get; set; }
        public string requestCreaterMail { get; set; }
        public string requestCreaterTelephone { get; set; }
        public DateTime? requestCreaterDateOfBorn { get; set; }
        public string requestCreaterPasportSeries { get; set; }
        public string requestCreaterPasportNumber { get; set; }
        public bool? requestCreaterOnBlackList { get; set; }
    }
}
