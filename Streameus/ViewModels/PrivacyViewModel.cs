using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    public class PrivacyViewModel
    {
        public bool? Email { get; set; }

        public bool? FirstName { get; set; }

        public bool? LastName { get; set; }

        public bool? Gender { get; set; }

        public bool? AbonnementsList { get; set; }

        public PrivacyViewModel()
        {
        }

        public PrivacyViewModel(User user)
        {
            this.AbonnementsList = user.AbonnementsVisibility;
            this.Email = user.EmailVisibility;
            this.FirstName = user.FirstNameVisibility;
            this.LastName = user.LastNameVisibility;
            this.Gender = user.GenderVisibility;
        }

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