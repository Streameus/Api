#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;

namespace Streameus.ViewModels
{
    // Models used as parameters to AccountController actions.


    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }
}

#pragma warning restore 1591