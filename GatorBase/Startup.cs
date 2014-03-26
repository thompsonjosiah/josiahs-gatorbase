using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GatorBase.Startup))]
namespace GatorBase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
