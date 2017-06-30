using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcProjesi.Startup))]
namespace MvcProjesi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
