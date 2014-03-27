using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Streameus.DataAbstractionLayer.Initializers;
using Streameus.Models.Mapping;

namespace Streameus.Models
{
    public partial class StreameusContext :
        IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        static StreameusContext()
        {
            Database.SetInitializer<StreameusContext>(null);
        }

        public StreameusContext()
            : base("Name=StreameusContext")
        {
            //Un initializer de db different est requis pour appHarbor, cf doc de la classe.
            var appHarbor = ConfigurationManager.AppSettings["Environment"] == "AppHarbor";
            if (appHarbor)
                Database.SetInitializer(new StreameusInitializerForAppHarbor());
            else
                Database.SetInitializer(new StreameusInitializer());
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<ConferenceParameters> ConferenceParameters { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Post> Posts { get; set; }
//        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ConferenceParametersMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ConferenceMap());
        }

        public static StreameusContext Create(IdentityFactoryOptions<StreameusContext> options, IOwinContext context)
        {
            return new StreameusContext();
        }
    }
}