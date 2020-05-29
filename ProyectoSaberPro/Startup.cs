using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoSaberPro.Startup))]
namespace ProyectoSaberPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
