using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for events
    /// </summary>
    public class EventMap : EntityTypeConfiguration<Event>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Events");
            this.Property(t => t.AuthorId).HasColumnName("AuthorId");

            // Relationships
            this.HasRequired(t => t.Author)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.AuthorId);
        }
    }
}