using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(course.Startup))]
namespace course
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
