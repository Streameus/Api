//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Streameus.Models
{
    public partial class ConferenceParameters
    {
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

        public int Id { get; set; }
        public double Price { get; set; }
        public int FreeTime { get; set; }
        public bool CanAskQuestions { get; set; }
        public bool CansAskVoiceQuestions { get; set; }
        public bool Visibility { get; set; }

        public virtual ICollection<User> FreeUsers { get; set; }
        public virtual ICollection<User> Intervenants { get; set; }
    }
}