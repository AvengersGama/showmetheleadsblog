using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gama.GrupoAvengers.Blog.Startup))]
namespace Gama.GrupoAvengers.Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
