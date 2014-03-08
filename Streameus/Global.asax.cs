using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Streameus.DataAbstractionLayer;
using Streameus.DataAbstractionLayer.Initializers;
using Streameus.DataBaseAccess;
using Streameus.Documentation;
using Streameus.Models;
using Streameus.ViewModels;
using Swashbuckle.Models;

namespace Streameus
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            this.RegisterIoC();
            this.SetInitializer();
            this.RegisterAutoMapping();
            this.SwaggerParameters();
        }

        private void SwaggerParameters()
        {
//            SwaggerSpecConfig.Customize(
//                c =>
//                {
//                    c.OperationFilter<AddStandardResponseMessages>();
//                    c.OperationFilter<AddAuthorizationResponseMessages>();
//                });
        }

        private void RegisterIoC()
        {
            var builder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();
            //Register api controllers
            builder.RegisterApiControllers(dataAccess);
            //Set UnitOfWork to exist for the whole duration of the API request
            builder.Register(u => new UnitOfWork(new StreameusContainer())).As<IUnitOfWork>().InstancePerApiRequest();
            //Register all the services
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Services"))
                .AsImplementedInterfaces();

            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);
            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private void RegisterAutoMapping()
        {
            Mapper.CreateMap<UserViewModel, User>();
        }

        private void SetInitializer()
        {
            //Un initializer de db different est requis pour appHarbor, cf doc de la classe.
            var appHarbor = ConfigurationManager.AppSettings["Environment"] == "AppHarbor";
            if (appHarbor)
                System.Data.Entity.Database.SetInitializer(new StreameusInitializerForAppHarbor());
            else
                System.Data.Entity.Database.SetInitializer(new StreameusInitializer());
        }
    }
}