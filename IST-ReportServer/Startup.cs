using Microsoft.Owin;
using Owin;
using WebservicePortal;

[assembly: OwinStartup(typeof(Startup))]

namespace WebservicePortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
