using System;
using System.Configuration;
using System.Data.Entity;
using System.Web.Management;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Streameus.DataAbstractionLayer.Initializers;
using Streameus.Models;
using Streameus.Models.Mapping;

namespace Streameus.DataAbstractionLayer
{
    /// <summary>
    /// The DBContext used by Streameus
    /// </summary>
    public partial class StreameusContext :
        IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public StreameusContext()
            : base("Name=StreameusContext")
        {
        }

        /// <summary>
        /// Comments Set
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// ConferenceParameters Set
        /// </summary>
        public DbSet<ConferenceParameters> ConferenceParameters { get; set; }

        /// <summary>
        /// ConferenceParameters Set
        /// </summary>
        public DbSet<ConferenceCategory> ConferenceCategories { get; set; }

        /// <summary>
        /// Conferences Set
        /// </summary>
        public DbSet<Conference> Conferences { get; set; }

        /// <summary>
        /// Documents Set
        /// </summary>
        public DbSet<Document> Documents { get; set; }

        /// <summary>
        /// Events Set
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Messages Groups Set
        /// </summary>
        public DbSet<MessageGroup> MessagesGroups { get; set; }

        /// <summary>
        /// Posts Set
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Maps table names, and sets up relationships between the various user entities
        /// </summary>
        /// <param name="modelBuilder"/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ConferenceParametersMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ConferenceMap());
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new MessageGroupMap());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StreameusContext, Migrations.Configuration>());

//            else
//            {
//                //Un initializer de db different est requis pour appHarbor, cf doc de la classe.
//                var appHarborDev = ConfigurationManager.AppSettings["Environment"] == "AppHarborDev";
//                var appHarbor = ConfigurationManager.AppSettings["Environment"] == "AppHarbor";
//                if (appHarborDev)
//                    Database.SetInitializer(new StreameusInitializerForAppHarbor());
//                else if (appHarbor)
//                    Database.SetInitializer(new CreateDatabaseIfNotExists<StreameusContext>());
//                else
//                    Database.SetInitializer(new StreameusInitializer());
//
//                new LogEvent("les valeurs de appharbor sont: dev(" + appHarborDev + "), prod(" + appHarbor + ")").Raise();
//                new LogEvent("Environment: " + ConfigurationManager.AppSettings["Environment"] + ".").Raise();
//            }
        }

        public class LogEvent : WebRequestErrorEvent
        {
            public LogEvent(string message)
                : base(null, null, 100001, new Exception(message))
            {
            }
        }

        /// <summary>
        /// Factory
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static StreameusContext Create(IdentityFactoryOptions<StreameusContext> options, IOwinContext context)
        {
            return new StreameusContext();
        }

        /// <summary>
        /// Mainly used to ease the testing
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>The DbSet for the requested entity</returns>
        public virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }
    }
}