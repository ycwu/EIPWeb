using System;
using System.Collections.Generic;
using System.Text;

namespace EIPApp.Models
{
    public class Chatroom
    {
        Guid ChatroomID { get; set; }
        string ChatroomName { get; set; }
        string ChatroomType { get; set; }
        DateTime CreateTime { get; set; }
        string CreateUser { get; set; }
        string CreateDevice { get; set; }
        DateTime ModifyTime { get; set; }
        string ModifyUser { get; set; }
        bool IsEnable { get; set; }
    }
}
