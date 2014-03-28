using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for conferenceparameters
    /// </summary>
    public class ConferenceParametersMap : EntityTypeConfiguration<ConferenceParameters>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConferenceParametersMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ConferenceParameters");

            // Relationships
            this.HasMany(t => t.Intervenants)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("ConferenceParametersIntervenants");
                    m.MapLeftKey("ConferenceParametersIntervenants_User_Id");
                    m.MapRightKey("Intervenants_Id");
                });

            this.HasMany(t => t.FreeUsers)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("ConferenceParametersUser");
                    m.MapLeftKey("ConferenceParametersUser_User_Id");
                    m.MapRightKey("FreeUsers_Id");
                });
//            this.HasRequired(t => t.Conference).WithRequiredDependent();
        }
    }
}