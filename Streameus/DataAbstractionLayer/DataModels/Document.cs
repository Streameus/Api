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
        public int Id { get; set; }
        public int ConferenceId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public int Downloads { get; set; }
        public System.DateTime UploadDate { get; set; }
        public Nullable<System.DateTime> LastDownloadDate { get; set; }
        public virtual Conference Conference { get; set; }
    }
}