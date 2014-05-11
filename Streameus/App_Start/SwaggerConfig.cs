using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Streameus;
using Streameus.Documentation;
using Swashbuckle.Application;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (SwaggerConfig), "Register")]

namespace Streameus
{
    /// <summary>
    /// Configure swagger
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Register all swagger related configs
        /// </summary>
        public static void Register()
        {
            Swashbuckle.Bootstrapper.Init(GlobalConfiguration.Configuration);
            SwaggerSpecConfig.Customize(
                c =>
                {
                    c.IgnoreObsoleteActions();
                    c.OperationFilter<AddStandardResponseMessages>();
                    c.OperationFilter<AddAuthorizationResponseMessages>();
                    c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/StreameusDocumentation.xml");
                });
            SwaggerUiConfig.Customize(c =>
            {
                c.InjectJavaScript(Assembly.GetExecutingAssembly(), "Streameus.Scripts.swagger.js");
                c.InjectStylesheet(Assembly.GetExecutingAssembly(), "Streameus.Content.swagger.css");
            });
        }
    }
}