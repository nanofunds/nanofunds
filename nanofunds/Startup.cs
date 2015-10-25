using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nanofunds.Startup))]
namespace nanofunds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
