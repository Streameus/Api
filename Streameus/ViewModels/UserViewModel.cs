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
            if (user.EmailVisibility)
                this.Email = user.Email;
            if (user.FirstNameVisibility)
                this.FirstName = user.FirstName;
            if (user.LastNameVisibility)
                this.LastName = user.LastName;
            this.Gender = user.GenderVisibility ? user.Gender : null;
            this.Reputation = user.Reputation;
            this.BirthDay = user.BirthDay;
            this.Phone = user.Phone;
            this.Address = user.Address;
            this.City = user.City;
            this.Country = user.Country;
            this.Website = user.Website;
            this.Profession = user.Profession;
            this.Description = user.Description;
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
        /// User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User gender, null if not said
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// reputation of the user
        /// </summary>
        public int Reputation { get; set; }

        /// <summary>
        /// User birth day
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// User phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// User city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// User country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// User website
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// User job
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        /// User Description
        /// </summary>
        public string Description { get; set; }
    }
}