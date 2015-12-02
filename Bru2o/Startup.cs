using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bru2o.Startup))]
namespace Bru2o
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
