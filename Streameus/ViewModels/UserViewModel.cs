using System;
using System.ComponentModel.DataAnnotations;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    ///     The user view model is returned to the client to map a user
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        ///     default constructor
        /// </summary>
        public UserViewModel()
        {
        }

        /// <summary>
        ///     Instantiate VM with a user's values
        /// </summary>
        /// <param name="user"></param>
        /// <param name="personal">If true, displays balance and every info</param>
        public UserViewModel(User user, bool personal = false)
        {
            this.Id = user.Id;
            this.Pseudo = user.Pseudo;
            this.Email = personal || user.EmailVisibility ? user.Email : null;
            this.FirstName = personal || user.FirstNameVisibility ? user.FirstName : null;
            this.LastName = personal || user.LastNameVisibility ? user.LastName : null;
            this.Gender = personal || user.GenderVisibility ? user.Gender : null;
            this.Reputation = user.Reputation;
            this.BirthDay = personal || user.BirthDayVisibility ? user.BirthDay : null;
            this.Phone = personal || user.PhoneVisibility ? user.Phone : null;
            this.Address = personal || user.AddressVisibility ? user.Address : null;
            this.City = personal || user.CityVisibility ? user.City : null;
            this.Country = personal || user.CountryVisibility ? user.Country : null;
            this.Website = personal || user.WebsiteVisibility ? user.Website : null;
            this.Profession = personal || user.ProfessionVisibility ? user.Profession : null;
            this.Description = personal || user.DescriptionVisibility ? user.Description : null;
            this.Followers = user.Followers.Count;
            this.Followings = user.Abonnements.Count;
            this.Conferences = user.ConferencesCreated.Count;
            this.Balance = personal ? user.Balance : (float?) null;

            this.DisplayName = this.FullName.Trim().Length > 0 ? this.FullName : this.Pseudo;
        }

        /// <summary>
        ///     User id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     User pseudo
        /// </summary>
        [Required]
        public string Pseudo { get; set; }

        /// <summary>
        ///     User email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     The display name to be used for this person
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        ///     Get the User's full name
        /// </summary>
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        /// <summary>
        ///     User gender, null if not said
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        ///     reputation of the user
        /// </summary>
        public int Reputation { get; set; }

        /// <summary>
        ///     User birth day
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        ///     User phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     User address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     User city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     User country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     User website
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        ///     User job
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        ///     User Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     User Followers
        /// </summary>
        public int Followers { get; set; }

        /// <summary>
        ///     User Followings
        /// </summary>
        public int Followings { get; set; }

        /// <summary>
        ///     User Conferences
        /// </summary>
        public int Conferences { get; set; }

        /// <summary>
        ///     The user balance
        /// </summary>
        public float? Balance { get; set; }
    }
}