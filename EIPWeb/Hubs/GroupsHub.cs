using EIPWeb.Helpers;
using EIPWeb.Models;
using EIPWeb.Models.Chat;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Transactions;

namespace EIPWeb.Hubs
{
    [HubName("GroupsHub")]
    public class GroupsHub : Hub
    {
        public static ChatContext DbContext = new ChatContext();

        #region IHub Members
        // 重写Hub连接事件  
        public override Task OnConnected()
        {

            // 查询用户
            var user = DbContext.Users.FirstOrDefault(u => u.UserId == Context.ConnectionId);
            if (user == null)
            {
                user = new Models.Chat.User
                {
                    ConnectionId = Guid.Parse(Context.ConnectionId)
                };
                DbContext.Users.Add(user);

                //儲存記錄
                NewChatClient();
            }

            // 发送房间列表
            var items = DbContext.Rooms.Select(p => new { p.RoomName });
            Clients.Client(this.Context.ConnectionId).getRoomList(JsonHelper.ToJsonString(items.ToList()));
            return base.OnConnected();
        }

        // 重写Hub连接断开的事件
        public override Task OnDisconnected(bool stopCalled)
        {
            // 查询用户
            var user = DbContext.Users.FirstOrDefault(u => u.UserId == Context.ConnectionId);

            if (user != null)
            {
                // 删除用户
                DbContext.Users.Remove(user);

                //儲存記錄
                RemoveChatClient();

                // 从房间中移除用户
                foreach (var item in user.Rooms)
                    RemoveUserFromRoom(item.RoomName);
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion 

        #region Public Methods

        // 为所有用户更新房间列表
        public void UpdateRoomList()
        {
            var itme = DbContext.Rooms.Select(p => new { p.RoomName });
            var jsondata = JsonHelper.ToJsonString(itme.ToList());
            Clients.All.getRoomlist(jsondata);
        }

        /// <summary>
        /// 加入聊天室
        /// </summary>
        public void JoinRoom(ChatRoom chatRoom)
        {
            // 查询聊天室
            var room = DbContext.Rooms.Find(p => p.RoomID == chatRoom.RoomID);

            // 存在则加入
            if (room == null) return;

            // 查找房间中是否存在此用户
            var isExistUser = room.Users.FirstOrDefault(u => u.ConnectionId == Guid.Parse(Context.ConnectionId));

            // 不存在则加入
            if (isExistUser == null)
            {
                var user = DbContext.Users.Find(u => u.ConnectionId == Guid.Parse(Context.ConnectionId));
                user.Rooms.Add(room);
                room.Users.Add(user);

                // 将客户端的连接ID加入到组里面
                Groups.Add(Context.ConnectionId, chatRoom.RoomID.ToString());

                //调用此连接用户的本地JS(显示房间)
                Clients.Client(Context.ConnectionId).joinRoom(chatRoom.RoomID.ToString());

                //儲存記錄
                JoinChatroom(chatRoom);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("请勿重复加入房间!");
            }
        }

        /// <summary>
        /// 创建聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public void CreateRoom(int roomID,string roomName)
        {
            string userID = Clients.CallerState.userID;

            var room = DbContext.Rooms.Find(a => a.RoomID == roomID);
            if (room == null)
            {
                var chatRoom = new ChatRoom
                {
                    RoomID = roomID,
                    RoomName = roomName
                };

                //将房间加入列表
                DbContext.Rooms.Add(chatRoom);

                // 本人加入聊天室
                JoinRoom(chatRoom);
                UpdateRoomList();
                //Clients.All.MessageReceived(roomName, $"新增聊天室成功:{roomName}");

                //儲存記錄-新增聊天室
                NewChatroom(roomID, userID);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("房间名重复!");
                // 本人加入聊天室
                var chatRoom = new ChatRoom
                {
                    RoomID = roomID,
                    RoomName = roomName
                };
                JoinRoom(chatRoom);
            }
        }

        /// <summary>
        /// 移除聊天室成員
        /// </summary>
        /// <param name="roomName"></param>
        public void RemoveUserFromRoom(string roomName)
        {
            //查找房间是否存在
            var room = DbContext.Rooms.Find(a => a.RoomName == roomName);

            //存在则进入删除
            if (room == null)
            {
                Clients.Client(Context.ConnectionId).showMessage("房间名不存在!");
                return;
            }

            // 查找要删除的用户
            var user = room.Users.FirstOrDefault(a => a.UserId == Context.ConnectionId);
            // 移除此用户
            room.Users.Remove(user);

            //儲存記錄-聊天室移除用戶端
            RemoveChatClient();

            //如果房间人数为0,则删除房间
            if (room.Users.Count <= 0)
            {
                DbContext.Rooms.Remove(room);

                RemoveChatroom(roomName);
            }

            Groups.Remove(Context.ConnectionId, roomName);

            //提示客户端
            Clients.Client(Context.ConnectionId).removeRoom("退出成功!");

            //儲存記錄-聊天室成員
            RemoveClientFromChatroom(roomName);

        }

        /// <summary>
        /// 给房间内所有的用户发送消息
        /// </summary>
        /// <param name="room">房间名</param>
        /// <param name="message">信息</param>
        /// 
        public void SendMessage(int roomID, string userName, string messageText) //Call from JS
        {
            Message message = new Message();
            message.ConnectionID = Guid.Parse(Context.ConnectionId);
            message.MessageTime = DateTime.Now;
            message.RoomID = roomID;
            message.UserName = userName;
            message.MessageText = messageText;
            SendMessage(message);
        }

        /// <summary>
        /// 给房间内所有的用户发送消息
        /// </summary>
        /// <param name="meesage">訊息物件</param>
        /// 
        public void SendMessage(Message message)    //Call from XamarinForms
        {
            // 调用房间内所有客户端的sendMessage方法
            // 因为在加入房间的时候，已经将客户端的ConnectionId添加到Groups对象中了，所有可以根据房间名找到房间内的所有连接Id
            // 其实我们也可以自己实现Group方法，我们只需要用List记录所有加入房间的ConnectionId
            // 然后调用Clients.Clients(connectionIdList),参数为我们记录的连接Id数组。
            Clients.Group(message.RoomID.ToString(), new string[0]).sendMessage(message.RoomID, message.MessageText, message.UserID);  //for WebPage
            Clients.Group(message.RoomID.ToString()).MessageReceived(message);  //for XamarinForms

            //儲存記錄-訊息檔
            NewChatMessage(message);
        }
        #endregion

        #region 儲存記錄
        //新增用戶端
        public void NewChatClient()
        {
            ChatClient chatClient = new ChatClient();
            chatClient.ConnectionID = Guid.Parse(Context.ConnectionId);
            chatClient.UserID = string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name) ? "" : HttpContext.Current.User.Identity.Name;
            repoChatClient.SaveChanges(chatClient);
        }

        //移除用戶端
        public void RemoveChatClient()
        {
            ChatClient chatClient = repoChatClient.Find(Guid.Parse(Context.ConnectionId));
            if (chatClient != null)
            {
                chatClient.IsEnable = false;
                repoChatClient.SaveChanges(chatClient);
            }
        }

        //加入聊天室
        public void JoinChatroom(ChatRoom chatRoom)
        {
            Chatroom chatroom = repoChatroom.Find(chatRoom.RoomID);
            if (chatroom != null)
            {
                ChatroomDetail chatroomDetail = repoChatroomDetail.Find(chatroom.ChatroomID, Guid.Parse(Context.ConnectionId));
                if (chatroomDetail == null)
                {
                    chatroomDetail = new ChatroomDetail();
                    chatroomDetail.ChatroomID = chatroom.ChatroomID;
                    chatroomDetail.ConnectionID = Guid.Parse(Context.ConnectionId);
                    repoChatroomDetail.SaveChanges(chatroomDetail);
                }
            }
        }

        //新增聊天室+成員
        public void NewChatroom(string roomName, string userID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //更新使用者代號
                ChatClient chatClient = repoChatClient.Find(Guid.Parse(Context.ConnectionId));
                if (string.IsNullOrEmpty(chatClient.UserID))
                {
                    chatClient.UserID = userID;
                    repoChatClient.SaveChanges(chatClient);
                }
                //新增聊天室
                Chatroom chatroom = repoChatroom.Find(roomName);
                if (chatroom == null)
                {
                    chatroom = new Chatroom();
                    chatroom.ChatroomName = roomName;
                    chatroom.ChatroomType = "S";
                    repoChatroom.SaveChanges(chatroom);
                }
                //新增聊天室成員
                ChatroomDetail chatroomDetail = repoChatroomDetail.Find(chatroom.ChatroomID, Guid.Parse(Context.ConnectionId));
                if (chatroomDetail == null)
                {
                    chatroomDetail = new ChatroomDetail();
                    chatroomDetail.ChatroomID = chatroom.ChatroomID;
                    chatroomDetail.ConnectionID = Guid.Parse(Context.ConnectionId);
                    repoChatroomDetail.SaveChanges(chatroomDetail);
                }
                scope.Complete();
            }
        }
        public void NewChatroom(int roomID, string userID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //更新使用者代號
                ChatClient chatClient = repoChatClient.Find(Guid.Parse(Context.ConnectionId));
                if (string.IsNullOrEmpty(chatClient.UserID))
                {
                    chatClient.UserID = userID;
                    repoChatClient.SaveChanges(chatClient);
                }
                //新增聊天室
                Chatroom chatroom = repoChatroom.Find(roomID);
                ChatroomDetail chatroomDetail = repoChatroomDetail.Find(chatroom.ChatroomID, Guid.Parse(Context.ConnectionId));
                if (chatroomDetail == null)
                {
                    chatroomDetail = new ChatroomDetail();
                    chatroomDetail.ChatroomID = chatroom.ChatroomID;
                    chatroomDetail.ConnectionID = Guid.Parse(Context.ConnectionId);
                    repoChatroomDetail.SaveChanges(chatroomDetail);
                }
                scope.Complete();
            }
        }

        //移除聊天室
        public void RemoveChatroom(string roomName)
        {
            ChatroomRepository repoChatroom = RepositoryHelper.GetChatroomRepository();
            Chatroom chatroom = repoChatroom.Find(roomName);
            if (chatroom != null)
                repoChatroom.RemoveRoom(chatroom);
        }       

        //移除聊天室成員
        public void RemoveClientFromChatroom(string roomName)
        {
            Chatroom chatroom = repoChatroom.Find(roomName);
            ChatroomDetail chatroomDetail = repoChatroomDetail.Find(chatroom.ChatroomID, Guid.Parse(Context.ConnectionId));
            if (chatroomDetail != null)
            {
                chatroomDetail.DisconnectTime = DateTime.Now;
                chatroomDetail.IsEnable = false;
                repoChatroomDetail.SaveChanges(chatroomDetail);
            }
        }

        //新增聊天訊息
        public void NewChatMessage(Message message)
        {
            ChatMessage chatMessage = new ChatMessage();
            chatMessage.ConnectionID = message.ConnectionID;
            chatMessage.ChatroomID = message.RoomID;
            chatMessage.Message = message.MessageText;
            chatMessage.MessageType = "M";
            //chatMessage.FileID = NULL;    //Not done
            repoChatMessage.SaveChanges(chatMessage);
        }

        //初始化+釋放資料存取
        ChatClientRepository repoChatClient = RepositoryHelper.GetChatClientRepository();
        ChatroomRepository repoChatroom = RepositoryHelper.GetChatroomRepository();
        ChatroomDetailRepository repoChatroomDetail = RepositoryHelper.GetChatroomDetailRepository();
        ChatMessageRepository repoChatMessage = RepositoryHelper.GetChatMessageRepository();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repoChatClient.UnitOfWork.Context.Dispose();
                repoChatroom.UnitOfWork.Context.Dispose();
                repoChatroomDetail.UnitOfWork.Context.Dispose();
                repoChatMessage.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}

/*
public override Task OnConnected()
{
   using (var db = new UserContext())
   {
       // Retrieve user.
       var user = db.Users.SingleOrDefault(u => u.UserName == Context.User.Identity.Name);

       // If user does not exist in database, must add.
       if (user == null)
       {
           user = new Models.User()
           {
               UserName = Context.User.Identity.Name
           };
           db.Users.Add(user);
           db.SaveChanges();
       }
       else
       {
           // Add to each assigned group.
           foreach (var item in user.Rooms)
           {
               Groups.Add(Context.ConnectionId, item.RoomName);
           }
       }
   }
   return base.OnConnected();
}        
*/
