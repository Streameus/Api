using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Streameus.Models.Mapping
{
    /// <summary>
    /// Mapping for Conference
    /// </summary>
    public class ConferenceCategoryMap : EntityTypeConfiguration<ConferenceCategory>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConferenceCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.Description)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ConferenceCategories");
        }
    }
}