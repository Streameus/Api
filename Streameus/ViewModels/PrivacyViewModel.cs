using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// The viewModel used to set the privacy options
    /// </summary>
    public class PrivacyViewModel
    {
        /// <summary>
        /// Is email public
        /// </summary>
        public bool? Email { get; set; }

        /// <summary>
        /// Is first name public?
        /// </summary>
        public bool? FirstName { get; set; }

        /// <summary>
        /// Is last name public?
        /// </summary>
        public bool? LastName { get; set; }

        /// <summary>
        /// Is gender public?
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// Is the abonnements list public?
        /// </summary>
        public bool? AbonnementsList { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PrivacyViewModel()
        {
        }

        /// <summary>
        /// Gets the user privacy value to create the VM
        /// </summary>
        /// <param name="user">The user to be used</param>
        public PrivacyViewModel(User user)
        {
            this.AbonnementsList = user.AbonnementsVisibility;
            this.Email = user.EmailVisibility;
            this.FirstName = user.FirstNameVisibility;
            this.LastName = user.LastNameVisibility;
            this.Gender = user.GenderVisibility;
        }

        /// <summary>
        /// Set a user privacy settings from a <see cref="PrivacyViewModel"/>
        /// </summary>
        /// <param name="user"></param>
        public void SetUserPrivacySettings(ref User user)
        {
            user.AbonnementsVisibility = this.AbonnementsList.HasValue
                ? this.AbonnementsList.Value
                : user.AbonnementsVisibility;
            user.EmailVisibility = this.Email.HasValue ? this.Email.Value : user.EmailVisibility;
            user.FirstNameVisibility = this.FirstName.HasValue ? this.FirstName.Value : user.FirstNameVisibility;
            user.LastNameVisibility = this.LastName.HasValue ? this.LastName.Value : user.LastNameVisibility;
            user.GenderVisibility = this.Gender.HasValue ? this.Gender.Value : user.GenderVisibility;
        }
    }
}