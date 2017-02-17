using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventorySystem.Startup))]
namespace InventorySystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
