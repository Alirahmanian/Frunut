using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrunutStock.Identification.Startup))]
namespace FrunutStock.Identification
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
