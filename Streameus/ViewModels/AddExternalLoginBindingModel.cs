#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;

namespace Streameus.ViewModels
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = @"External access token")]
        public string ExternalAccessToken { get; set; }
    }
}

#pragma warning restore 1591