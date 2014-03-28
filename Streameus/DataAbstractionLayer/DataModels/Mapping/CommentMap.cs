using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for the comments
    /// </summary>
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
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