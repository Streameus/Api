//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel.DataAnnotations.Schema;

namespace Streameus.Models
{
    /// <summary>
    /// Parameters used for an user.
    /// This is a complex type, which is merged into user in the DB
    /// </summary>
    /// <remarks>
    /// Because it's a Complex type, this class cannot contain collection
    /// </remarks>
    [ComplexType]
    public partial class Parameters
    {
        public bool NotifMail { get; set; }
    }
}