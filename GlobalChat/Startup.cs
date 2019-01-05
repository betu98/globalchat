using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GlobalChat.Startup))]
namespace GlobalChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}