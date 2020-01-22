using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iTunesSearch.Startup))]
namespace iTunesSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
