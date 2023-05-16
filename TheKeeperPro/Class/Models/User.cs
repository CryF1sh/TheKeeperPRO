using System;
using System.Collections.Generic;

namespace TheKeeperPro.Class
{
    public partial class User
    {
        public int userId { get; set; }

        public string surname { get; set; }

        public string name { get; set; }

        public string patronymic { get; set; }

        public string mail { get; set; } 

        public string login { get; set; }

        public string password { get; set; } 

        public bool? onBlackList { get; set; }

        public string telephone { get; set; } 

        public DateTime dateOfBorn { get; set; }

        public string pasportSeries { get; set; } 

        public string pasportNumber { get; set; } 

        public byte[] visitersPhote { get; set; }

        public byte[] photoOfPasport { get; set; }

        public virtual ICollection<ListOfVisiter> listOfVisiters { get; set; } = new List<ListOfVisiter>();

        public virtual ICollection<Message> messages { get; set; } = new List<Message>();
    }

}

