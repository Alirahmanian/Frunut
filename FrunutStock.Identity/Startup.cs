using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrunutStock.Identity.Startup))]
namespace FrunutStock.Identity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
