using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for the User class
    /// </summary>
    public class UserMap : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Pseudo)
                .IsRequired();

            this.Property(t => t.Email)
                .IsRequired();

            this.Property(t => t.FirstName)
                .IsRequired();

            this.Property(t => t.LastName)
                .IsRequired();

            this.Property(t => t.Phone)
                .IsRequired();

            this.Property(t => t.Address)
                .IsRequired();

            this.Property(t => t.City)
                .IsRequired();

            this.Property(t => t.Country)
                .IsRequired();

            this.Property(t => t.Website)
                .IsRequired();

            this.Property(t => t.Profession)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Users");

            // Relationships
            this.HasMany(t => t.Followers)
                .WithMany(t => t.Abonnements)
                .Map(m =>
                {
                    m.ToTable("AbonnementsFollowers");
                    m.MapLeftKey("Followers_Id");
                    m.MapRightKey("Abonnements_Id");
                });
        }
    }
}