using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EIPWeb.Hubs
{
    [HubName("Sample2Hub")]
    public class Sample2 : Hub
    {

        //宣告靜態類別，來儲存上線清單
        public static class UserHandler
        {
            public static Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();
        }

        public static class MessageHandler
        {
            public static List<Models.Chat.Sample2Message> MessageList = new List<Models.Chat.Sample2Message>();
        }

        //使用者連現時呼叫
        public void userConnected(string name)
        {
            //進行編碼，防止XSS攻擊
            name = HttpUtility.HtmlEncode(name);
            string message = "歡迎使用者 " + name + " 加入聊天室";

            //發送訊息給除了自己的其他使用者
            Clients.Others.hello(message);

            //發送訊息至全部更新上線清單,發送自己取得上線清單
            Clients.All.addList(name);
            foreach (KeyValuePair<string, string> item in UserHandler.ConnectedIds)
                Clients.Caller.addList(item.Value);

            //新增目前使用者至上線清單
            UserHandler.ConnectedIds.Add(Context.ConnectionId, name);

            //新增至其它人員清單
            Clients.Others.addBox(Context.ConnectionId, name);

            //新增至自己人員清單(扣除自己)
            var userList = UserHandler.ConnectedIds.Where(p => p.Key != Context.ConnectionId).ToList();
            foreach (KeyValuePair< string, string> item in userList)
                Clients.Caller.addBox(item.Key, item.Value);
        }

        //發送訊息給所有人
        public void sendAllMessage(string message)
        {
            message = HttpUtility.HtmlEncode(message);
            var name = UserHandler.ConnectedIds.Where(p => p.Key == Context.ConnectionId).FirstOrDefault().Value;
            message = name + "說：" + message;
            Clients.All.sendAllMessage(message);
        }


        //發送訊息至特定使用者
        public void sendMessage(string ToId, string message)
        {
            message = HttpUtility.HtmlEncode(message);
            var fromName = UserHandler.ConnectedIds.Where(p => p.Key == Context.ConnectionId).FirstOrDefault().Value;
            message = fromName + " <span style='color:red'>悄悄的對你說</span>：" + message;
            Clients.Client(ToId).sendMessage(message);
        }

        public void Hello(string name)
        {
            Clients.All.hello(name);
        }
        //當使用者斷線時呼叫
        //public override Task OnDisconnected()
        //{
        //    //當使用者離開時，移除在清單內的 ConnectionId
        //    Clients.All.removeList(Context.ConnectionId);
        //    UserHandler.ConnectedIds.Remove(Context.ConnectionId);
        //    return base.OnDisconnected();
        //}
    }
}