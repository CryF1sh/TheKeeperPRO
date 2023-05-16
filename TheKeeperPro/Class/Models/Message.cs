using System;
using System.Collections.Generic;

namespace TheKeeperPro.Class
{
    public partial class Message
    {
        public int messageId { get; set; }

        public int userId { get; set; }

        public string message1 { get; set; }

        public virtual User user { get; set; }
    }

}

