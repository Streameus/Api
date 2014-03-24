using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// The user view model is returned to the client to map a user
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public UserViewModel()
        {
        }

        /// <summary>
        /// Instantiate VM with a user's values
        /// </summary>
        /// <param name="user"></param>
        public UserViewModel(User user)
        {
            this.Id = user.Id;
            this.Pseudo = user.Pseudo;
            this.Email = user.EmailVisibility ? user.Email : null;
            this.FirstName = user.FirstNameVisibility ? user.FirstName : null;
            this.LastName = user.LastNameVisibility ? user.LastName : null;
            this.Gender = user.GenderVisibility ? user.Gender : null;
            this.Reputation = user.Reputation;
            this.BirthDay = user.BirthDayVisibility ? user.BirthDay : null;
            this.Phone = user.PhoneVisibility ? user.Phone : null;
            this.Address = user.AddressVisibility ? user.Address : null;
            this.City = user.CityVisibility ? user.City : null;
            this.Country = user.CountryVisibility ? user.Country : null;
            this.Website = user.WebsiteVisibility ? user.Website : null;
            this.Profession = user.ProfessionVisibility ? user.Profession : null;
            this.Description = user.DescriptionVisibility ? user.Description : null;
        }

        /// <summary>
        /// User id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User pseudo
        /// </summary>
        [Required]
        public string Pseudo { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User email visibility
        /// </summary>
        public Boolean EmailVisibility { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User first name visibility
        /// </summary>
        public Boolean FirstNameVisibility { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User last name visibility
        /// </summary>
        public Boolean LastNameVisibility { get; set; }

        /// <summary>
        /// User gender, null if not said
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// User gender, null if not said
        /// </summary>
        public Boolean GenderVisibility { get; set; }

        /// <summary>
        /// reputation of the user
        /// </summary>
        public int Reputation { get; set; }

        /// <summary>
        /// User birth day
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// User birth day
        /// </summary>
        public Boolean BirthDayVisibility { get; set; }

        /// <summary>
        /// User phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// User phone
        /// </summary>
        public Boolean PhoneVisibility { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        public Boolean AddressVisibility { get; set; }

        /// <summary>
        /// User city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// User city
        /// </summary>
        public Boolean CityVisibility { get; set; }

        /// <summary>
        /// User country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// User country visibility
        /// </summary>
        public Boolean CountryVisibility { get; set; }

        /// <summary>
        /// User website
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// User website Visibility
        /// </summary>
        public Boolean WebsiteVisibility { get; set; }

        /// <summary>
        /// User job
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        /// User job visibility
        /// </summary>
        public Boolean ProfessionVisibility { get; set; }

        /// <summary>
        /// User Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// User DescriptionVisibility
        /// </summary>
        public Boolean DescriptionVisibility { get; set; }
    }
}