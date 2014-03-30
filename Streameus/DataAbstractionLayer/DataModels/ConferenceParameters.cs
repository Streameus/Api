//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Streameus.Models
{
    /// <summary>
    /// Parameters class for a conference
    /// </summary>
    public partial class ConferenceParameters
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConferenceParameters()
        {
            this.Price = 0D;
            this.FreeTime = 0;
            this.CanAskQuestions = true;
            this.CansAskVoiceQuestions = true;
            this.Visibility = true;
            this.FreeUsers = new HashSet<User>();
            this.Intervenants = new HashSet<User>();
        }

        /// <summary>
        /// Id used for db storage
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The price of the conference
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Free watching time for a paid conference
        /// </summary>
        public int FreeTime { get; set; }

        /// <summary>
        /// Can people type questions?
        /// </summary>
        public bool CanAskQuestions { get; set; }

        /// <summary>
        /// Can people speak to ask questions?
        /// </summary>
        public bool CansAskVoiceQuestions { get; set; }

        /// <summary>
        /// Is this conference public?
        /// </summary>
        public bool Visibility { get; set; }

        /// <summary>
        /// The list of user allowed to watch this conference for free
        /// </summary>
        public virtual ICollection<User> FreeUsers { get; set; }

        /// <summary>
        /// List of people helping for the conference 
        /// </summary>
        public virtual ICollection<User> Intervenants { get; set; }
    }
}