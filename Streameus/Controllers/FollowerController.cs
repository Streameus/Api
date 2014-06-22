using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;
using NoResultException = Streameus.Exceptions.NoResultException;

namespace Streameus.Controllers
{
    /// <summary>
    /// Controller to get the followers of an user
    /// </summary>
    public class FollowerController : BaseController
    {
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        public FollowerController(IUserServices userServices)
        {
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._userServices = userServices;
        }

        // GET api/follower/5
        /// <summary>
        /// Get all the followers for an user
        /// </summary>
        /// <param name="id">The user id</param>
        /// <exception cref="Exceptions.HttpErrors.NoResultException">If the user doesn't have any followers</exception>
        /// <returns></returns>
        /// <exception cref="NoResultException">If the user doesn't have any followers</exception>
        /// <exception cref="NotFoundException">If the user doesn't exists</exception>
        public IEnumerable<UserViewModel> Get(int id)
        {
            var followersViewModels = new List<UserViewModel>();
            try
            {
                var followers = this._userServices.GetFollowersForUser(id);
                followers.ForEach(f => followersViewModels.Add(new UserViewModel(f)));
            }
            catch (EmptyResultException)
            {
                //Now this case doesn't matter anymore.
                //Sends back an empty array instead
            }
            catch (NoResultException e)
            {
                throw new Exceptions.HttpErrors.NotFoundException(e.Message);
            }
            return followersViewModels;
        }
    }
}