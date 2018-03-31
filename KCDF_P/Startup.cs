using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KCDF_P.Startup))]
namespace KCDF_P
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
