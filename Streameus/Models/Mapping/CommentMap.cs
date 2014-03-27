using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Message)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Comments");

            // Relationships
            this.HasRequired(t => t.Post)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.PostId);
            this.HasRequired(t => t.Author).WithOptional();
        }
    }
}