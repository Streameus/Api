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
    /// <summary>
    /// User class
    /// </summary>
    public partial class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public User()
        {
            this.Language = 0;
            this.EmailVisibility = true;
            this.FirstName = "";
            this.FirstNameVisibility = true;
            this.LastName = "";
            this.LastNameVisibility = true;
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
            this.ConferencesInvolved = new HashSet<Conference>();
            this.ConferencesAttended = new HashSet<Conference>();
            this.ConferencesRegistered = new HashSet<Conference>();
            this.Events = new HashSet<Event>();
            this.Abonnements = new HashSet<User>();
            this.Followers = new HashSet<User>();
            this.MessageGroups = new HashSet<MessageGroup>();
            this.UnreadMessages = new HashSet<MessageGroup>();
            this.SentMessages = new HashSet<Message>();
            this.Parameters = new Parameters();
        }

        /// <summary>
        /// User Id
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// Pseudo
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// User language
        /// </summary>
        public int Language { get; set; }

        /// <summary>
        /// User email adress
        /// </summary>
        public override string Email { get; set; }

        /// <summary>
        /// Email visibility
        /// </summary>
        public bool EmailVisibility { get; set; }

        /// <summary>
        /// User firstname
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// First name visibility
        /// </summary>
        public bool FirstNameVisibility { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Last name visibility
        /// </summary>
        public bool LastNameVisibility { get; set; }

        /// <summary>
        /// User gendre
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// Gender visibility
        /// </summary>
        public bool GenderVisibility { get; set; }

        /// <summary>
        /// User reputation
        /// </summary>
        public int Reputation { get; set; }

        /// <summary>
        /// Abonnements visibility
        /// </summary>
        public bool AbonnementsVisibility { get; set; }

        /// <summary>
        /// Birthday visibility
        /// </summary>
        public bool BirthDayVisibility { get; set; }

        /// <summary>
        /// Phone visibility
        /// </summary>
        public bool PhoneVisibility { get; set; }

        /// <summary>
        /// Address visibility
        /// </summary>
        public bool AddressVisibility { get; set; }

        /// <summary>
        /// City visibility
        /// </summary>
        public bool CityVisibility { get; set; }

        /// <summary>
        /// Country visibility
        /// </summary>
        public bool CountryVisibility { get; set; }

        /// <summary>
        /// Website visibility
        /// </summary>
        public bool WebsiteVisibility { get; set; }

        /// <summary>
        /// Profession visibility
        /// </summary>
        public bool ProfessionVisibility { get; set; }

        /// <summary>
        /// Description visibility
        /// </summary>
        public bool DescriptionVisibility { get; set; }

        /// <summary>
        /// User birth day
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// User phone number
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
        /// User profession
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        /// User description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Conferences created
        /// </summary>
        public virtual ICollection<Conference> ConferencesCreated { get; set; }

        /// <summary>
        /// Conferences involved as a Speaker
        /// </summary>
        public virtual ICollection<Conference> ConferencesInvolved { get; set; }

        /// <summary>
        /// Conferences attended as a Participant
        /// </summary>
        public virtual ICollection<Conference> ConferencesAttended { get; set; }

        /// <summary>
        /// Conferences RSVP as a Participant
        /// </summary>
        public virtual ICollection<Conference> ConferencesRegistered { get; set; }

        /// <summary>
        /// User posts
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }

        /// <summary>
        /// User abonnements
        /// </summary>
        public virtual ICollection<User> Abonnements { get; set; }

        /// <summary>
        /// User followers
        /// </summary>
        public virtual ICollection<User> Followers { get; set; }

        /// <summary>
        /// User unread messages
        /// </summary>
        public virtual ICollection<MessageGroup> UnreadMessages { get; set; }

        /// <summary>
        /// User sent messages
        /// </summary>
        public virtual ICollection<Message> SentMessages { get; set; }

        /// <summary>
        /// User group messages
        /// </summary>
        public virtual ICollection<MessageGroup> MessageGroups { get; set; }

        /// <summary>
        /// User parameters
        /// </summary>
        public Parameters Parameters { get; set; }

        /// <summary>
        /// the stripe token of the user's credit card
        /// </summary>
        public String StripeTokenId { get; set; }

        /// <summary>
        /// User balance, used to pay conferences
        /// </summary>
        public float Balance { get; set; }

        /// <summary>
        /// User name, uses Email (lowercased) to prevent duplication of content
        /// </summary>
        public override string UserName
        {
            get { return Email; }
            set { Email = value.ToLower(); }
        }
    }
}