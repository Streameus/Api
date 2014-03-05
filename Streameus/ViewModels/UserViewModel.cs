using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

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
            if (user.GenderVisibility)
                this.Gender = user.Gender;
            else
                this.Gender = null;
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

        public int Id { get; set; }

        [Required]
        public string Pseudo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Gender { get; set; }
        public int Reputation { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
    }
}