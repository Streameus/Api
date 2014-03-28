//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Streameus.Models
{
    public partial class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public User()
        {
            this.Language = 0;
            this.EmailVisibility = true;
            this.FirstName = "";
            this.FirstNameVisibility = true;
            this.LastName = "";
            this.LastNameVisibility = true;
            this.PicturePath = "";
            this.GenderVisibility = true;
            this.Reputation = 0;
            this.AbonnementsVisibility = true;
            this.BirthDayVisibility = true;
            this.PhoneVisibility = true;
            this.AddressVisibility = true;
            this.CityVisibility = true;
            this.CountryVisibility = true;
            this.WebsiteVisibility = true;
            this.ProfessionVisibility = true;
            this.DescriptionVisibility = true;
            this.Phone = "";
            this.Address = "";
            this.City = "";
            this.Country = "";
            this.Website = "";
            this.Profession = "";
            this.Description = "";
            this.ConferencesCreated = new HashSet<Conference>();
            this.Conferences = new HashSet<Conference>();
            this.Posts = new HashSet<Post>();
            this.Abonnements = new HashSet<User>();
            this.Followers = new HashSet<User>();
            this.Parameters = new Parameters();
        }

        public override int Id { get; set; }
        public string Pseudo { get; set; }
        public int Language { get; set; }
        public override string Email { get; set; }
        public bool EmailVisibility { get; set; }
        public string FirstName { get; set; }
        public bool FirstNameVisibility { get; set; }
        public string LastName { get; set; }
        public bool LastNameVisibility { get; set; }
        public string PicturePath { get; set; }
        public bool? Gender { get; set; }
        public bool GenderVisibility { get; set; }
        public int Reputation { get; set; }
        public bool AbonnementsVisibility { get; set; }
        public bool BirthDayVisibility { get; set; }
        public bool PhoneVisibility { get; set; }
        public bool AddressVisibility { get; set; }
        public bool CityVisibility { get; set; }
        public bool CountryVisibility { get; set; }
        public bool WebsiteVisibility { get; set; }
        public bool ProfessionVisibility { get; set; }
        public bool DescriptionVisibility { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Conference> ConferencesCreated { get; set; }
        public virtual ICollection<Conference> Conferences { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<User> Abonnements { get; set; }
        public virtual ICollection<User> Followers { get; set; }
        public Parameters Parameters { get; set; }

        /// <summary>
        /// User name, uses Pseudo to prevent duplication of content
        /// </summary>
        public override string UserName
        {
            get { return Pseudo; }
            set { Pseudo = value; }
        }
    }
}