using System;
using System.Collections.Generic;

namespace EIPWeb.Models.Chat
{
    /// <summary>
    /// 上下文类，用来模拟EF中的DbContext
    /// </summary>
    public class ChatContext
    {
        public List<User> Users { get; set; }

        public List<Connection> Connections { get; set; }

        public List<ChatRoom> Rooms { get; set; }

        public ChatContext()
        {
            Users = new List<User>();
            Connections = new List<Connection>();
            Rooms = new List<ChatRoom>();
        }
    }

    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户連接Id
        /// </summary>
        public Guid ConnectionId { get; set; }

        /// <summary>
        /// 用户的连接集合
        /// </summary>
        public List<Connection> Connections { get; set; }

        /// <summary>
        /// 用户房间集合，一个用户可以加入多个房间
        /// </summary>
        public List<ChatRoom> Rooms { get; set; }

        public User()
        {
            Connections = new List<Connection>();
            Rooms = new List<ChatRoom>();
        }
    }

    public class Connection
    {
        //连接ID
        public string ConnectionId { get; set; }

        //用户代理
        public string UserAgent { get; set; }

        //是否连接
        public bool Connected { get; set; }
    }

    /// <summary>
    /// 房间类
    /// </summary>
    public class ChatRoom
    {
        // 房間代號
        public Guid RoomID { get; set; }

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