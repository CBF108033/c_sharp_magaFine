using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace magaFine
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 新增 Article update disploy 的路由設定
            routes.MapRoute(
                name: "ArticleUpdateDisployRoute",
                url: "Article/UpdateDisploy",
                defaults: new { controller = "Article", action = "UpdateDisploy" }
            );

            // 新增 Article Delete 的路由設定
            routes.MapRoute(
                name: "ArticleDeleteRoute",
                url: "Article/Delete/{id}",
                defaults: new { controller = "Article", action = "Delete", id = UrlParameter.Optional }
            );

            // 新增 Article Create 的路由設定
            routes.MapRoute(
                name: "ArticleCreateRoute",
                url: "Article/create",
                defaults: new { controller = "Article", action = "Create" }
            );

            //新增 Article Details 的路由設定
            routes.MapRoute(
                name: "ArticleDetails",
                url: "article/{id}",
                defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            );

            // 新增 Article 的路由設定
            routes.MapRoute(
                name: "ArticleRoute",
                url: "Article/{userId}",
                defaults: new { controller = "Article", action = "Index", userId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
