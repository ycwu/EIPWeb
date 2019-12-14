using EIPWeb.Features.SamplePersistentConnection;
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
            //SignalR Scaleout with SQL Server,https://tpu.thinkpower.com.tw/tpu/File/html/201607/20160719120839_f.html?f=3dj6j8kd38895ksgtdddd93865jhr9sn3rqkh
            //string sqlConnectionString = @"data source=localhost\sqlexpress;initial catalog=EIPDB;user id=XamarinForms;password=XamarinForms;";
            //GlobalHost.DependencyResolver.UseSqlServer(sqlConnectionString);            
            app.MapSignalR();

            //app.MapSignalR<DemoPersistentConnection>("/Connections/DemoPersistentConnection");
            //app.MapSignalR<AuthorizationPersistentConnection>("/Connections/AuthorizationPersistentConnection");

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
