using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions.HttpErrors;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;
using NoResultException = Streameus.Exceptions.NoResultException;

namespace Streameus.Controllers
{
    public class FollowerController : BaseController
    {
        private readonly IUserServices _userServices;

        public FollowerController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        // GET api/follower/5
        public IEnumerable<UserViewModel> Get(int id)
        {
            var followersViewModels = new List<UserViewModel>();
            try
            {
                var followers = this._userServices.GetFollowersForUser(id);
                followers.ForEach(f => followersViewModels.Add(new UserViewModel(f)));
            }
            catch (NoResultException e)
            {
                throw new NotFoundException(e.Message);
            }
            return followersViewModels;
        }
    }
}