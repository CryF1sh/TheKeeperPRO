using System;
using System.Collections.Generic;

namespace TheKeeperPro.Class
{
    public partial class Division
    {
        public int divisionId { get; set; }

        public string name { get; set; }

        public virtual ICollection<Employer> employers { get; set; } = new List<Employer>();
    }
}


