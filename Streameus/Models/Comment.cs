using System;
using System.Collections.Generic;

namespace Streameus.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public string Message { get; set; }
        public System.DateTime Date { get; set; }
        public virtual Post Post { get; set; }
        public virtual User Author { get; set; }
    }
}