using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for posts
    /// </summary>
    public class MessageGroupMap : EntityTypeConfiguration<MessageGroup>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("MessageGroups");

            // Relationships
            this.HasMany(t => t.Members)
                .WithMany(t => t.MessageGroups)
                .Map(m =>
                {
                    m.ToTable("MessagesGroupsMembers");
                    m.MapLeftKey("Group_Id");
                    m.MapRightKey("Member_Id");
                });

            this.HasMany(t => t.UnreadBy)
                .WithMany(t => t.UnreadMessages)
                .Map(m =>
                {
                    m.ToTable("MessageGroupReaders");
                    m.MapLeftKey("MessageGroup_Id");
                    m.MapRightKey("User_Id");
                });

        }
    }
}