using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(admin.Startup))]
namespace admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
