using System;
namespace TheKeeperPro.Class
{
    public class ListOfVisitersResponse
    {
        public int listOfVisitersId { get; set; }
        public int requestId { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public string userMail { get; set; }
        public string userTelephone { get; set; }
        public byte[] usersPhoto { get; set; }
        public Nullable<bool> creater { get; set; }
    }
}
