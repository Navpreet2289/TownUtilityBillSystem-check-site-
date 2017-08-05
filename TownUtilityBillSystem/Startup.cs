using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TownUtilityBillSystem.Startup))]
namespace TownUtilityBillSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
