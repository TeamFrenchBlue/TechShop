﻿namespace TechShop.WebApi
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Microsoft.Owin.Security.OAuth;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("http://localhost:18768", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}/{username}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    controller = RouteParameter.Optional,
                    action = RouteParameter.Optional,
                    username = RouteParameter.Optional
                }
            );
        }
    }
}
