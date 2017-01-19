using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MobileMart.Startup))]
namespace MobileMart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
