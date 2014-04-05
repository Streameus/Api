//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace Streameus.Models
{
    /// <summary>
    /// Represent a message from a user to another
    /// </summary>
    /// <remarks>
    /// Likely to be changed pretty soon
    /// </remarks>
    public partial class MessageGroup
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageGroup()
        {
            this.Members = new HashSet<User>();
            this.Messages = new HashSet<Message>();
        }

        /// <summary>
        /// Db ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The messages in this message group
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }

        /// <summary>
        /// The users in this message group
        /// </summary>
        public virtual ICollection<User> Members { get; set; }
    }
}