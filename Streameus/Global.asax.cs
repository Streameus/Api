﻿using System.Data.Entity;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Streameus.Hooks;
using Swashbuckle.Application;
#pragma warning disable 1591
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

namespace Streameus
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            this.RegisterIoC();
            this.RegisterAutoMapping();
            //Register handler for Ajax PUT/DELETE request
            GlobalConfiguration.Configuration.MessageHandlers.Add(new XHttpMethodOverrideDelegatingHandler());
//            this.SetJsonSerializerProperties();
        }

        //Balance le json en CamelCase
        private void SetJsonSerializerProperties()
        {
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }

        private void RegisterIoC()
        {
            var builder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();
            //Register api controllers
            builder.RegisterApiControllers(dataAccess);
            //Set UnitOfWork to exist for the whole duration of the API request
            builder.Register(u => new UnitOfWork(new StreameusContext())).As<IUnitOfWork>().InstancePerRequest();
            //Register all the services
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Services"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (Stripe.StripeCharge)));

            // Build the IOC container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);
            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private void RegisterAutoMapping()
        {
            Mapper.CreateMap<UserViewModel, User>()
                .ForMember(m => m.Followers, config => config.Ignore())
                .ForSourceMember(m => m.Followers, config => config.Ignore())
                .ForSourceMember(m => m.Followings, config => config.Ignore());
        }
    }
}

#pragma warning restore 1591