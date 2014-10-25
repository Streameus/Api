using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataAbstractionLayer.Services;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;
using NoResultException = Streameus.Exceptions.NoResultException;

namespace Streameus.Controllers
{
    /// <summary>
    /// The controller to see/set an users abonnements
    /// </summary>
    public class FollowingController : BaseController
    {
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        public FollowingController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        // GET api/following/5
        /// <summary>
        /// Get all the people this user is following
        /// </summary>
        /// <param name="id">the user id</param>
        /// <param name="options">OData query options</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [Authorize]
        public List<UserViewModel> Get(int id, ODataQueryOptions<User> options = null)
        {
            var abonnementsViewModels = new List<UserViewModel>();
            try
            {
                var abonnements = this._userServices.GetAbonnementsForUser(id);
                if (options != null)
                    abonnements = options.ApplyTo(abonnements) as IQueryable<User>;
                abonnements.ForEach(f => abonnementsViewModels.Add(new UserViewModel(f)));
            }
            catch (NoResultException e)
            {
                throw new NotFoundException(e.Message);
            }
            catch (EmptyResultException)
            {
                //Now this case doesn't matter anymore.
                //Sends back an empty array instead
            }
            return abonnementsViewModels;
        }

        // POST api/following
        /// <summary>
        /// Follow somebody
        /// </summary>
        /// <param name="id">the id of the user to follow</param>
        ///  <response code="409">You cannot follow yourself</response>
        [Authorize]
        public bool Post(int id)
        {
            var userId = this.GetCurrentUserId();
            return this._userServices.AddFollowing(userId, id);
        }

        // DELETE api/following/5
        /// <summary>
        /// Stop following somebody
        /// </summary>
        /// <param name="id">the id of the user to stop following</param>
        /// <exception cref="NotImplementedException"></exception>
        [Authorize]
        public bool Delete(int id)
        {
            var userId = this.GetCurrentUserId();
            return this._userServices.RemoveFollowing(userId, id);
        }
    }
}