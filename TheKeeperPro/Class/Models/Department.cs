using System;
using System.Collections.Generic;

namespace TheKeeperPro.Class
{
    public partial class Department
    {
        public int departmentId { get; set; }

        public string name { get; set; }

        public virtual ICollection<Employer> employers { get; set; } = new List<Employer>();
    }
}


