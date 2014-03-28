using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mappings for Documents
    /// </summary>
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired();

            this.Property(t => t.Path)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Documents");

            // Relationships
            this.HasRequired(t => t.Conference)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.ConferenceId);
        }
    }
}