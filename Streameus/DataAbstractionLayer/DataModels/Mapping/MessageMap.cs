using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for posts
    /// </summary>
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Messages");
            this.Property(t => t.SenderId).HasColumnName("SenderId");
            this.Property(t => t.GroupId).HasColumnName("GroupId");

            // Relationships
            this.HasRequired(t => t.Sender)
                .WithMany(t => t.SentMessages)
                .HasForeignKey(d => d.SenderId);
            this.HasRequired(t => t.Group)
                .WithMany(t => t.Messages)
                .HasForeignKey(d => d.GroupId);
        }
    }
}