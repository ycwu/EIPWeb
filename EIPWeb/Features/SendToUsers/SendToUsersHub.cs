using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace EIPWeb.Features.SendToUsers
{
    public class SendToUsersHub : Hub
    {
        public void SendToUsers(IList<string> userIds, string value)
        {
            Clients.Users(userIds).message(value);
        }
    }
}