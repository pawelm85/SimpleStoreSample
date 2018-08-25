using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleStoreSample.Startup))]
namespace SimpleStoreSample
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
