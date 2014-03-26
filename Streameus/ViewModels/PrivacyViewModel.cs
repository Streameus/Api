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
        /// is the User birth day visible?
        /// </summary>
        public Boolean? BirthDay { get; set; }

        /// <summary>
        /// is the User phone visible?
        /// </summary>
        public Boolean? Phone { get; set; }

        /// <summary>
        /// is the User address visible?
        /// </summary>
        public Boolean? Address { get; set; }

        /// <summary>
        /// is the User city visible?
        /// </summary>
        public Boolean? City { get; set; }

        /// <summary>
        /// is the User country visible?
        /// </summary>
        public Boolean? Country { get; set; }

        /// <summary>
        /// is the User website Visible?
        /// </summary>
        public Boolean? Website { get; set; }

        /// <summary>
        /// is the User job visible?
        /// </summary>
        public Boolean? Profession { get; set; }

        /// <summary>
        /// is the User Description Visible ?
        /// </summary>
        public Boolean? Description { get; set; }

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
            this.BirthDay = user.BirthDayVisibility;
            this.Phone = user.PhoneVisibility;
            this.Address = user.AddressVisibility;
            this.City = user.CityVisibility;
            this.Country = user.CountryVisibility;
            this.Website = user.WebsiteVisibility;
            this.Profession = user.ProfessionVisibility;
            this.Description = user.DescriptionVisibility;
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
            user.BirthDayVisibility = this.BirthDay.HasValue ? this.BirthDay.Value : user.BirthDayVisibility;
            user.PhoneVisibility = this.Phone.HasValue ? this.Phone.Value : user.PhoneVisibility;
            user.AddressVisibility = this.Address.HasValue ? this.Address.Value : user.AddressVisibility;
            user.CityVisibility = this.City.HasValue ? this.City.Value : user.CityVisibility;
            user.CountryVisibility = this.Country.HasValue ? this.Country.Value : user.CountryVisibility;
            user.WebsiteVisibility = this.Website.HasValue ? this.Website.Value : user.WebsiteVisibility;
            user.ProfessionVisibility = this.Profession.HasValue ? this.Profession.Value : user.ProfessionVisibility;
            user.DescriptionVisibility = this.Description.HasValue ? this.Description.Value : user.DescriptionVisibility;
        }
    }
}