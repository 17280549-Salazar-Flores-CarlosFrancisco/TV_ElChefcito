using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tienda_Virtual_El_Chefcito.Startup))]
namespace Tienda_Virtual_El_Chefcito
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
