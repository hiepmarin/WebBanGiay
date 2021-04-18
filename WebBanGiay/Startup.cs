using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanGiay.Startup))]
namespace WebBanGiay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
