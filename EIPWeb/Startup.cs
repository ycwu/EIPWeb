using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using WebApplication.Features.Authorization;
using WebApplication.Features.SamplePersistentConnection;

[assembly: OwinStartupAttribute(typeof(EIPWeb.Startup))]
namespace EIPWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            app.MapSignalR<DemoPersistentConnection>("/Connections/DemoPersistentConnection");
            app.MapSignalR<AuthorizationPersistentConnection>("/Connections/AuthorizationPersistentConnection");

            app.Map("/EnableDetailedErrors", map =>
            {
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };

                map.MapSignalR(hubConfiguration);
            });
        }
    }
}
