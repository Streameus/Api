//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Streameus.Models
{
    //prevent documentation warning for autogenerated code
    #pragma warning disable 1591
    
    using System;
    using System.Collections.Generic;
    
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
    #pragma warning restore 1591
}
