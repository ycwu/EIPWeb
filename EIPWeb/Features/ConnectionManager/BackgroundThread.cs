using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EIPWeb.Features.SamplePersistentConnection;
using Microsoft.AspNet.SignalR;
using WebApplication.Features.SamplePersistentConnection;

namespace EIPWeb.Features.SampleHub
{
    public class BackgroundThread
    {
        public static bool Enabled { get; set; }

        public static async Task SendOnPersistentConnection()
        {
            var context = GlobalHost.ConnectionManager.GetConnectionContext<DemoPersistentConnection>();

            while (Enabled)
            {
                await context.Connection.Broadcast("BackgroundThread.SendOnPersistentConnection sending message on " + DateTime.Now.ToString("HH:mm:ss"));
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        public static async Task SendOnHub()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<DemoHub>();

            while (Enabled)
            {
                await context.Clients.All.hubMessage("BackgroundThread.SendOnHub sending message on " + DateTime.Now.ToString("HH:mm:ss"));
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }
    }
}