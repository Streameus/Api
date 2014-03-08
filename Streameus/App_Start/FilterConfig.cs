#pragma warning disable 1591
using System.Web.Mvc;

namespace Streameus
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

#pragma warning restore 1591