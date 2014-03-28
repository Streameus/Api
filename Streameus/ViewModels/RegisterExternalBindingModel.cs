using System.ComponentModel.DataAnnotations;

namespace Streameus.ViewModels
{
    /// <summary>
    /// Class used for creating an account to a OAuthed user
    /// </summary>
    public class RegisterExternalBindingModel
    {
        /// <summary>
        /// User name to be used (default is taken from the OAuth credentials)
        /// </summary>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }
}