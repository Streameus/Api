//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using Streameus.Enums;

namespace Streameus.Models
{
    /// <summary>
    /// Represent an event that happened
    /// </summary>
    public partial class Event
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Event()
        {
            this.Comments = new HashSet<Comment>();
            this.EventItems = new HashSet<EventItem>();
        }

        /// <summary>
        /// Db ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Is this post private or public?
        /// </summary>
        public bool Visibility { get; set; }

        /// <summary>
        /// Type of event
        /// </summary>
        public Enums.DataBaseEnums.EventType Type { get; set; }

        /// <summary>
        /// Date this event happened
        /// </summary>
        public System.DateTime Date { get; set; }

        /// <summary>
        /// Id of the user who generated this event
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// List of comment linked to this event
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// List of event Type linked to this event
        /// </summary>
        public virtual ICollection<EventItem> EventItems { get; set; } 

        /// <summary>
        /// The user who generated this event
        /// </summary>
        public virtual User Author { get; set; }
    }
}