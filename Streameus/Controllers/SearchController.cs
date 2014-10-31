using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;
using NoResultException = Streameus.Exceptions.NoResultException;

namespace Streameus.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [RoutePrefix("api/Search")]
    public class SearchController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IConferenceServices _conferenceServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="conferenceServices"></param>
        public SearchController(IUserServices userServices, IConferenceServices conferenceServices)
        {
            if (conferenceServices == null) throw new ArgumentNullException("conferenceServices");
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._userServices = userServices;
            this._conferenceServices = conferenceServices;
        }

        // GET api/search
        /// <summary>
        /// Search on users and conferences
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public SearchResultViewModel Get(string query, ODataQueryOptions options = null)
        {
            var keywords = QueryToKeywords(query);
            var userList = SearchInUsers(keywords, options);
            var confList = SearchInConferences(keywords, options);
            return new SearchResultViewModel(confList, userList);
        }

        // GET api/search/user
        /// <summary>
        /// Search on users
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("Users")]
        public UserViewModel[] GetUsers(string query, ODataQueryOptions options = null)
        {
            var keywords = QueryToKeywords(query);
            var userList = SearchInUsers(keywords, options);
            return userList.ToArray();
        }

        // GET api/search/user
        /// <summary>
        /// Search on conferences
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("Conferences")]
        public ConferenceViewModel[] GetConferences(string query, ODataQueryOptions options = null)
        {
            var keywords = QueryToKeywords(query);
            var confList = SearchInConferences(keywords, options);
            return confList.ToArray();
        }

        private string[] QueryToKeywords(string query)
        {
            var filteredQuery = FilterQuery(query);
            return filteredQuery.Split(' ');
        }

        private string FilterQuery(string query)
        {
            return Regex.Replace(query, @"\s+", " ").ToLower();
        }

        private List<UserViewModel> SearchInUsers(IEnumerable<string> keywords, ODataQueryOptions options = null)
        {
            var userList = new List<UserViewModel>();
            var users = _userServices.GetAll()
                .Where(u => keywords.All(k => 
                    u.FirstName.ToLower().Contains(k)
                    || u.Description.ToLower().Contains(k)
                    || u.FirstName.ToLower().Contains(k)
                    || u.LastName.ToLower().Contains(k)
                    || u.Pseudo.ToLower().Contains(k)));
            if (options != null)
                users = options.ApplyTo(users) as IQueryable<User>;

            users.ForEach(u => userList.Add(new UserViewModel(u)));
            return userList;
        }

        private List<ConferenceViewModel> SearchInConferences(IEnumerable<string> keywords, ODataQueryOptions options = null)
        {
            var confList = new List<ConferenceViewModel>();
            var confs = _conferenceServices.GetAll()
                .Where(c => keywords.All(k =>
                    c.Name.ToLower().Contains(k)
                    || c.Description.ToLower().Contains(k)));
            if (options != null)
                confs = options.ApplyTo(confs) as IQueryable<Conference>;
            
            confs.ForEach(c => confList.Add(new ConferenceViewModel(c)));
            return confList;
        }

    }
}