//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Streameus.Models
{
    public partial class Conference
    {
        public Conference()
        {
            this.Documents = new HashSet<Document>();
            this.Members = new HashSet<User>();
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Enums.DataBaseEnums.ConfStatus Status { get; set; }
        public System.DateTime Time { get; set; }
        public int ScheduledDuration { get; set; }
        public int FinalDuration { get; set; }

        public virtual ConferenceParameters ConferenceParameters { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<User> Members { get; set; }
    }
}