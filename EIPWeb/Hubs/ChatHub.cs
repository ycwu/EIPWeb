using EIPWeb.Models;
using EIPWeb.Models.Chat;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EIPWeb.Hubs
{
    //[Authorize]
    [HubName("ChatHub")]
    public class Chat : Hub
    {
        public override Task OnConnected()
        {
            using (var db = new UserContext())
            {
                // Retrieve user.
                var user = db.Users
                    .Include(u => u.Rooms)
                    .SingleOrDefault(u => u.UserName == Context.User.Identity.Name);

                // If user does not exist in database, must add.
                if (user == null)
                {
                    user = new Models.User()
                    {
                        UserName = Context.User.Identity.Name,
                        Connections = new List<Models.Connection>()
                    };
                    db.Users.Add(user);
                }
                else
                {
                    // Add to each assigned group.
                    foreach (var item in user.Rooms)
                    {
                        Groups.Add(Context.ConnectionId, item.RoomName);
                    }
                }

                user.Connections.Add(new Models.Connection
                {
                    ConnectionID = Context.ConnectionId,
                    UserAgent = Context.Request.Headers["User-Agent"],
                    Connected = true
                });
                //db.SaveChanges();
            }
            return base.OnConnected();
        }

        public void CreateRoom(string roomName)
        {
            using (var db = new UserContext())
            {
                var room = db.Rooms.Find(roomName);
                if (room == null)
                {
                    var cr = new ConversationRoom
                    {
                        RoomName = roomName
                    };

                    //将房间加入列表
                    db.Rooms.Add(cr);
                    db.SaveChanges();

                    // 本人加入聊天室
                    JoinRoom(roomName);
                    //UpdateRoomList();
                    //Clients.All.MessageReceived(roomName, $"新增聊天室成功:{roomName}");
                }
                else
                {
                    Clients.Client(Context.ConnectionId).showMessage("房间名重复!");
                    // 本人加入聊天室
                    JoinRoom(roomName);
                }
            }
        }

        public void JoinRoom(string roomName)
        {
            using (var db = new UserContext())
            {
                // Retrieve room.
                var room = db.Rooms.Find(roomName);

                if (room != null)
                {
                    var user = new Models.User() { UserName = Context.User.Identity.Name };
                    db.Users.Attach(user);

                    room.Users.Add(user);
                    db.SaveChanges();
                    Groups.Add(Context.ConnectionId, roomName);
                }
            }
        }

        public void RemoveUserFromRoom(string roomName)
        {
            using (var db = new UserContext())
            {
                // Retrieve room.
                var room = db.Rooms.Find(roomName);
                if (room != null)
                {
                    var user = new Models.User() { UserName = Context.User.Identity.Name };
                    db.Users.Attach(user);

                    room.Users.Remove(user);
                    db.SaveChanges();

                    Groups.Remove(Context.ConnectionId, roomName);
                }
            }
        }

        public void SendMessage(string roomID, string userName, string messageText) //Call from JS
        {
            Message message = new Message();
            //message.MessageID = Guid.NewGuid();
            message.MessageTime = DateTime.Now;
            message.RoomID = int.Parse(roomID);
            message.UserName = userName;
            message.MessageText = messageText;
            SendMessage(message);
        }

        public void SendMessage(Message message)    //Call from XamarinForms
        {
            // 调用房间内所有客户端的sendMessage方法
            // 因为在加入房间的时候，已经将客户端的ConnectionId添加到Groups对象中了，所有可以根据房间名找到房间内的所有连接Id
            // 其实我们也可以自己实现Group方法，我们只需要用List记录所有加入房间的ConnectionId
            // 然后调用Clients.Clients(connectionIdList),参数为我们记录的连接Id数组。
            Clients.Group(message.RoomID.ToString(), new string[0]).sendMessage(message.RoomID, message.MessageText);  //for WebPage
            Clients.Group(message.RoomID.ToString()).MessageReceived(message);  //for XamarinForms
        }
    }
}