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
    public partial class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public bool Visibility { get; set; }
        public string Content { get; set; }
        public System.DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User Author { get; set; }
    }
}