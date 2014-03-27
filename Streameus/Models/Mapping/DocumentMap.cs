using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
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