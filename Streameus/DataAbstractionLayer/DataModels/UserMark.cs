//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Streameus.Models
{
    /// <summary>
    /// A class representing mark given to a conference by a user
    /// </summary>
    public class UserMark
    {
        /// <summary>
        /// UserMarks id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the user who marked the conference
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id of the conference marked
        /// </summary>
        public int ConferenceId { get; set; }

        /// <summary>
        /// The mark given by the user
        /// </summary>
        [DefaultValue(0)]
        [Range(0, 5)]
        public double Mark { get; set; }

        /// <summary>
        /// The user who marked the conference
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// The conference marked
        /// </summary>
        public virtual Conference Conference { get; set; }
    }
}