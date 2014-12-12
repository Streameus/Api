using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// A view model for a search result
    /// </summary>
    public class SearchResultViewModel
    {
        /// <summary>
        /// Users
        /// </summary>
        public UserViewModel[] Users { get; set; }

        /// <summary>
        /// Conferences
        /// </summary>
        public ConferenceViewModel[] Conferences { get; set; }

        /// <summary>
        /// Create a vm based on a conference
        /// </summary>
        /// <param name="conferences"></param>
        /// <param name="users"></param>
        public SearchResultViewModel(List<ConferenceViewModel> conferences, List<UserViewModel> users)
        {
            this.Users = users.ToArray();
            this.Conferences = conferences.ToArray();
        }
    }
}