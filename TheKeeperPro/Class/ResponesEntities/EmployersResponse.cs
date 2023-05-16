using System;
namespace TheKeeperPro.Class
{
    public class EmployersResponse
    {
        public int employeeId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public Nullable<int> departmentId { get; set; }
        public string departmentName { get; set; }
        public Nullable<int> divisionId { get; set; }
        public string divisionName { get; set; }
    }
}
