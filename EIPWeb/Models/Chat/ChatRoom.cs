using System.Collections.Generic;

namespace EIPWeb.Models.Chat
{
    /// <summary>
    /// 房间类
    /// </summary>
    public class ChatRoom
    {
        // 房間代號
        //public string RoomID { get; set; }

        // 房间名称
        public string RoomName { get; set; }

        // 用户集合
        public List<User> Users { get; set; }

        public ChatRoom()
        {
            Users = new List<User>();
        }
    }
}