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
    /// Represent an event that happened
    /// </summary>
    /// <remarks>
    /// Likely to be changed pretty soon
    /// </remarks>
    public partial class Post
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Post()
        {
            this.Comments = new HashSet<Comment>();
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
        /// Post content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Date this event happened
        /// </summary>
        public System.DateTime Date { get; set; }

        /// <summary>
        /// Id of the user who generated this event
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// List of comment linked to this post
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// The user who generated this event
        /// </summary>
        public virtual User Author { get; set; }
    }
}