using System.Configuration;
using System.Data.Entity;
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
        /// Conferences Set
        /// </summary>
        public DbSet<Conference> Conferences { get; set; }

        /// <summary>
        /// Documents Set
        /// </summary>
        public DbSet<Document> Documents { get; set; }

        /// <summary>
        /// Posts Set
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Maps table names, and sets up relationships between the various user entities
        /// </summary>
        /// <param name="modelBuilder"/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Un initializer de db different est requis pour appHarbor, cf doc de la classe.
            var appHarbor = ConfigurationManager.AppSettings["Environment"] == "AppHarbor";
            if (appHarbor)
                Database.SetInitializer(new StreameusInitializerForAppHarbor());
            else
                Database.SetInitializer(new StreameusInitializer());

            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ConferenceParametersMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ConferenceMap());
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