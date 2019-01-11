using DotNetNuke.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hugsys.ECPay
{
    public class RouteConfig : IServiceRouteMapper

    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("ECPay",
                "default",
                "{controller}/{action}",
                new[] { "Hugsys.ECPay.Controllers" });
        }
    }
}