using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataAbstractionLayer.Services;
using Streameus.Exceptions;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;

namespace Streameus.Controllers
{
    public class FollowingController : BaseController
    {
        private readonly IUserServices _userServices;

        public FollowingController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        // GET api/following/5
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
                HttpErrors.NotFound(e.Message);
            }
            return abonnementsViewModels;
        }

        // POST api/following
        public void Post([FromBody] string value)
        {
            //Todo implementer apres l'auth
            throw new NotImplementedException("Revenez quand l'auth marchera");
        }

        // DELETE api/following/5
        public void Delete(int id)
        {
            //Todo implementer apres l'auth
            throw new NotImplementedException("Revenez quand l'auth marchera");
        }
    }
}