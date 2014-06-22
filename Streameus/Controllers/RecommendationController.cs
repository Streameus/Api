using System;
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

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RecommendationController(IUserServices userServices)
        {
            if (userServices == null) throw new ArgumentNullException("userServices"); 
            this._userServices = userServices;
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
    }
}