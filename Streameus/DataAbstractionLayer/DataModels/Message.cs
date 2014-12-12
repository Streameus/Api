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
    public partial class Message
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Message()
        {
        }

        /// <summary>
        /// Db ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Message content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Date of this message
        /// </summary>
        public System.DateTime Date { get; set; }

        /// <summary>
        /// Id of the user who sent this message
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Id of the message group
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// The user who sent this message
        /// </summary>
        public virtual User Sender { get; set; }

        /// <summary>
        /// The group of this message
        /// </summary>
        public virtual MessageGroup Group { get; set; }
    }
}