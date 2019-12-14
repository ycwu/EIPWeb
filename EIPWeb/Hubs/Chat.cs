using EIPWeb.Models.Chat;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
//using Microsoft.AspNet.SignalR.Hubs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

namespace EIPWeb.Hubs
{
    [HubName("ChatHub")]
    public class Chat : Hub
    {
        //Sample1
        public void Send(string Message)
        {
            Clients.All.addMessage(Message);
        }        
        //public void SendGroupMessage(string room, string message)
        //{
        //    Clients.Group(room, new string[0]).sendMessage(room, message + " " + DateTime.Now);
        //}
        public void Hello(string name)
        {
            string message = $"歡迎使用者{name}加入聊天室";
            Clients.All.hello(message);
        }

        public void SendMessage(Message message)
        {
            Clients.All.MessageReceived(message.UserName, message.MessageText);
        }

        public void SendMessage(string username, string message)
        {
            Clients.All.MessageReceived(username, message);
        }
    }
}