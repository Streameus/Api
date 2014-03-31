//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Streameus.Models
{
    /// <summary>
    /// Class representing user's comments on posts
    /// </summary>
    public partial class Comment
    {
        /// <summary>
        /// Comment ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the post
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Id of the autor
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Comment message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Comment date
        /// </summary>
        public System.DateTime Date { get; set; }

        /// <summary>
        /// Post object
        /// </summary>
        public virtual Post Post { get; set; }

        /// <summary>
        /// Author object
        /// </summary>
        public virtual User Author { get; set; }
    }
}