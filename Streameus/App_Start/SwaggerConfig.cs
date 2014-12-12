using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Streameus;
using Streameus.Documentation;
using Streameus.Models;
using Streameus.ViewModels;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof (SwaggerConfig), "Register")]

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
            var queryOptionDataType = new DataType()
            {
                Type = "ODataQueryOptions",
                Description = "Options to filter request : $skip, $top, $filter, $orderBy",
            };
            SwaggerSpecConfig.Customize(
                c =>
                {
                    c.IgnoreObsoleteActions();
                    c.OperationFilter<AddStandardResponseMessages>();
                    c.OperationFilter<AddAuthorizationResponseMessages>();
                    c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/StreameusDocumentation.xml");
                    // Custom mapping for ODataQueryOptions
                    c.MapType<ODataQueryOptions<Event>>(() => queryOptionDataType);
                    c.MapType<ODataQueryOptions<Conference>>(() => queryOptionDataType);
                    c.MapType<ODataQueryOptions<User>>(() => queryOptionDataType);
                    c.MapType<ODataQueryOptions<Message>>(() => queryOptionDataType);
                    c.MapType<ODataQueryOptions<MessageGroup>>(() => queryOptionDataType);
                    c.MapType<ODataQueryOptions<MessageGroupViewModel>>(() => queryOptionDataType);
                });


            SwaggerUiConfig.Customize(c =>
            {
                c.InjectJavaScript(Assembly.GetExecutingAssembly(), "Streameus.Scripts.swagger.js");
                c.InjectStylesheet(Assembly.GetExecutingAssembly(), "Streameus.Content.swagger.css");
            });
        }
    }
}