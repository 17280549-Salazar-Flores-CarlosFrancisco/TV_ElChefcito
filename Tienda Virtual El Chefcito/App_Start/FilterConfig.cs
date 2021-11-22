using System.Web;
using System.Web.Mvc;

namespace Tienda_Virtual_El_Chefcito
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
