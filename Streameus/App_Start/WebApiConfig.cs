﻿using Microsoft.Owin.Security.OAuth;
#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Streameus.Areas.HelpPage;
using Streameus.Documentation;
using Streameus.Hooks;
using XmlDocumentationProvider = Streameus.Documentation.XmlDocumentationProvider;

namespace Streameus
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API configuration and services
            //Permet de controler la generation de la doc XML, est utilise pour swagger
            config.SetDocumentationProvider(new XmlDocumentationProvider(
                HttpContext.Current.Server.MapPath("~/App_Data/StreameusDocumentation.xml")));


            //Verifier automatiquement le model a chaque requete
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new ApiExceptionFilterAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {Controller = "ApiHome", id = RouteParameter.Optional}
                );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}

#pragma warning restore 1591