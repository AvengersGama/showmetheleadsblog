using System.Web;
using System.Web.Mvc;

namespace Gama.GrupoAvengers.Blog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
