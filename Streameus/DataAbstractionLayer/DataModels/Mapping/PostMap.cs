using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for posts
    /// </summary>
    public class PostMap : EntityTypeConfiguration<Post>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PostMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Posts");
            this.Property(t => t.AuthorId).HasColumnName("AuthorId");

            // Relationships
            this.HasRequired(t => t.Author)
                .WithMany(t => t.Posts)
                .HasForeignKey(d => d.AuthorId);
        }
    }
}