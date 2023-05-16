using System;
using System.Collections.Generic;

namespace TheKeeperPro.Class
{
    public partial class ListOfVisiter
    {
        public int listOfVisitersId { get; set; }

        public int requestId { get; set; }

        public int userId { get; set; }

        public bool? creater { get; set; }

        public virtual Request request { get; set; }

        public virtual User user { get; set; }
    }
}


