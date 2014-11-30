using System.Configuration;
using Streameus.DataAbstractionLayer;
using Streameus.DataAbstractionLayer.Initializers;
using Streameus.Models;

namespace Streameus.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StreameusContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Streameus.DataAbstractionLayer.StreameusContext";
        }

        protected override void Seed(StreameusContext context)
        {
            //TODO uncomment this if you wanna seed
            if (!context.GetDbSet<User>().Any())
                StreameusSeeder.Seed(context);
        }
    }
}