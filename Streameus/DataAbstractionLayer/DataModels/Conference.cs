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
    public partial class Conference
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Conference()
        {
            this.Documents = new HashSet<Document>();
            this.Speakers = new HashSet<User>();
            this.Participants = new HashSet<User>();
        }

        /// <summary>
        /// Conference ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Conference's owner ID
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// Conference's owner ID
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Conference name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Conference Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Conference Status
        /// </summary>
        public Enums.DataBaseEnums.ConfStatus Status { get; set; }
        /// <summary>
        /// Start Date and Time for the Conference
        /// </summary>
        public System.DateTime Time { get; set; }
        /// <summary>
        /// Estimated duration for the Conference
        /// </summary>
        public int ScheduledDuration { get; set; }
        /// <summary>
        /// Final duration for the Conference (set at the end)
        /// </summary>
        public int FinalDuration { get; set; }


        /// <summary>
        /// Conference Parameters
        /// </summary>
        public virtual ConferenceParameters ConferenceParameters { get; set; }
        /// <summary>
        /// Collection of Documents usefull for the Conference
        /// </summary>
        public virtual ICollection<Document> Documents { get; set; }
        /// <summary>
        /// Conference Owner
        /// </summary>
        public virtual User Owner { get; set; }
        /// <summary>
        /// Conference Category
        /// </summary>
        public virtual ConferenceCategory Category { get; set; }
        /// <summary>
        /// Speakers participating to the Conference
        /// </summary>
        public virtual ICollection<User> Speakers { get; set; }
        /// <summary>
        /// Users participating to the Conference
        /// </summary>
        public virtual ICollection<User> Participants { get; set; }
    }

}