using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FacilitiesCheckOut.Startup))]
namespace FacilitiesCheckOut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
