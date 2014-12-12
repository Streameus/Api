//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Streameus.Enums;

namespace Streameus.Models
{
    /// <summary>
    /// Conference
    /// </summary>
    public partial class ConferenceCategory
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ConferenceCategory()
        {
            this.Conferences = new HashSet<Conference>();
        }

        /// <summary>
        /// Conference ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Conference category name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Conference category description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Conference of that category
        /// </summary>
        public virtual ICollection<Conference> Conferences { get; set; }
    }

}