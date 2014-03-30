//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System;

namespace Streameus.Models
{
    public partial class Document
    {
        /// <summary>
        ///  Document ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of the conference
        /// </summary>
        public int ConferenceId { get; set; }
        /// <summary>
        /// Document file name
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Document path
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Document's size
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Number of download for this document 
        /// </summary>
        public int Downloads { get; set; }
        /// <summary>
        /// Date of upload for this document
        /// </summary>
        public System.DateTime UploadDate { get; set; }
        /// <summary>
        /// Last download date for this document
        /// </summary>
        public Nullable<System.DateTime> LastDownloadDate { get; set; }
        /// <summary>
        /// Conference object
        /// </summary>
        public virtual Conference Conference { get; set; }
    }
}