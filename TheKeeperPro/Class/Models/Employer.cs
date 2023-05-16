using System;
using System.Collections.Generic;

namespace TheKeeperPro.Class
{
    public partial class Employer
    {
        public int employeeId { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public int? departmentId { get; set; }

        public int? divisionId { get; set; }

        public virtual Department department { get; set; }

        public virtual Division division { get; set; }

        public virtual ICollection<Request> requests { get; set; } = new List<Request>();

        public override string ToString()
        {
            return surname + " " + name;
        }
    }
}


