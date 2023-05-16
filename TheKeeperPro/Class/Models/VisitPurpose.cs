using System;
using System.Collections.Generic;

namespace TheKeeperPro.Class
{
    public partial class VisitPurpose
    {
        public int visitPurposeId { get; set; }

        public string name { get; set; } 

        public virtual ICollection<Request> requests { get; set; } = new List<Request>();
    }
}


