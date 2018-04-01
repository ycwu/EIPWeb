using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
//using Microsoft.AspNet.SignalR.Hubs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

namespace EIPWeb.Hubs
{
    public class Chat : Hub
    {
        //public void SendGroupMessage(string room, string message)
        //{
        //    Clients.Group(room, new string[0]).sendMessage(room, message + " " + DateTime.Now);
        //}

        public void SendMessage(string username, string message)
        {
            Clients.All.MessageReceived(username, message);
        }
    }
}