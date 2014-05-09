using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for Conference
    /// </summary>
    public class ConferenceMap : EntityTypeConfiguration<Conference>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConferenceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.Description)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Conferences");

            // Relationships
            this.HasMany(t => t.Speakers)
                .WithMany(t => t.Conferences)
                .Map(m =>
                {
                    m.ToTable("ConferenceSpeakers");
                    m.MapLeftKey("Conference_Id");
                    m.MapRightKey("Speaker_Id");
                });

            this.HasMany(t => t.Participants)
                .WithMany(t => t.Conferences)
                .Map(m =>
                {
                    m.ToTable("ConferenceParticipants");
                    m.MapLeftKey("Conference_Id");
                    m.MapRightKey("Participant_Id");
                });

            this.HasRequired(t => t.Owner)
                .WithMany(t => t.ConferencesCreated)
                .HasForeignKey(d => d.OwnerId).WillCascadeOnDelete(false);

            this.HasRequired(t => t.Category)
                .WithMany(t => t.Conferences)
                .HasForeignKey(d => d.CategoryId).WillCascadeOnDelete(false);
        }
    }
}