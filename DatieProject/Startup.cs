using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DatieProject.Startup))]
namespace DatieProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
