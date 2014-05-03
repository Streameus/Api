using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public List<UserViewModel> Get(int id)
        {
            var abonnementsViewModels = new List<UserViewModel>();
            try
            {
                var abonnements = this._userServices.GetAbonnementsForUser(id);
                abonnements.ForEach(f => abonnementsViewModels.Add(new UserViewModel(f)));
            }
            catch (NoResultException e)
            {
                throw new NotFoundException(e.Message);
            }
            catch (EmptyResultException e)
            {
                throw new Exceptions.HttpErrors.NoResultException(e.Message);
            }
            return abonnementsViewModels;
        }

        // POST api/following
        /// <summary>
        /// Follow somebody
        /// </summary>
        /// <param name="id">the id of the user to follow</param>
        /// <exception cref="NotImplementedException"></exception>
        [Authorize]
        public bool Post(int id)
        {
            var userId = Convert.ToInt32(this.User.Identity.GetUserId());
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
            var userId = Convert.ToInt32(this.User.Identity.GetUserId());
            return this._userServices.RemoveFollowing(userId, id);
        }
    }
}