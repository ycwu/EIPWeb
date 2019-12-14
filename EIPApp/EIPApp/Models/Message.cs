using System;
using System.Collections.Generic;
using System.Text;

namespace EIPApp.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public DateTime MessageTime { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public Guid ConnectionID { get; set; }
        public string MessageText { get; set; }
    }
}
