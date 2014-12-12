using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for UserMark
    /// </summary>
    public class UserMarkMap : EntityTypeConfiguration<UserMark>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserMarkMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Mark)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("UserMarks");

            // Relationships
            this.HasRequired(t => t.Conference)
                .WithMany(t => t.Marks)
                .HasForeignKey(t => t.ConferenceId).WillCascadeOnDelete(false);

            this.HasRequired(t => t.User);
        }
    }
}