﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;

namespace Streameus.Controllers
{
    /// <summary>
    /// The controller used to get the recommendations
    /// </summary>
    [RoutePrefix("api/Recommendation")]
    public class RecommendationController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IConferenceServices _conferenceServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="conferenceServices"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RecommendationController(IUserServices userServices, IConferenceServices conferenceServices)
        {
            if (userServices == null) throw new ArgumentNullException("userServices");
            if (conferenceServices == null) throw new ArgumentNullException("conferenceServices");
            this._userServices = userServices;
            this._conferenceServices = conferenceServices;
        }

        /// <summary>
        /// Get recommended users to follow
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("users")]
        public IEnumerable<UserViewModel> GetUserRecommendations()
        {
            var suggestionList = new List<UserViewModel>();
            IEnumerable<User> suggestions = this._userServices.GetSuggestionsForUser(this.GetCurrentUserId());
            suggestions.ForEach(s => suggestionList.Add(new UserViewModel(s)));
            if (!suggestionList.Any())
            {
                suggestions = this._userServices.GetUsersWithBestReputation();
                suggestions.ForEach(s => suggestionList.Add(new UserViewModel(s)));
            }
            return suggestionList;
        }

        /// <summary>
        /// Get recommended conferences to attend
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("conferences")]
        public IEnumerable<ConferenceViewModel> GetConferenceRecommendations()
        {
            var suggestionList = new List<ConferenceViewModel>();
            var currentUser = this.GetCurrentUserId();
            var suggestions = this._conferenceServices.GetSuggestionsForUser(currentUser);


            suggestions.ForEach(s => suggestionList.Add(new ConferenceViewModel(s, currentUser)));
            if (!suggestionList.Any())
            {
                suggestions = this._conferenceServices.GetMostPopularConfs();
                suggestions.ForEach(s => suggestionList.Add(new ConferenceViewModel(s, currentUser)));
            }
            return suggestionList;
        }
    }
}