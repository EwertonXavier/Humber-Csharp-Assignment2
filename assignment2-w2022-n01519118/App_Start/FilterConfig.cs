using System.Web;
using System.Web.Mvc;

namespace assignment2_w2022_n01519118
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
