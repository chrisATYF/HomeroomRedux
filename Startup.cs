using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeroomRedux.Startup))]
namespace HomeroomRedux
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
