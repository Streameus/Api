using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions;
using Streameus.ViewModels;

namespace Streameus.Controllers
{
    /// <summary>
    /// Controller in charge of the privacy settings
    /// </summary>
    public class PrivacyController : BaseController
    {
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        public PrivacyController(IUserServices userServices)
        {
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._userServices = userServices;
        }

        // GET api/privacy/5
        /// <summary>
        /// Get privacy settings for an user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>the user's privacy settigns</returns>
        public PrivacyViewModel Get(int id)
        {
            return new PrivacyViewModel(this._userServices.GetById(id));
        }

        // Put api/privacy
        /// <summary>
        /// Update an user's privacy settings. Only specified values are taken into account
        /// </summary>
        /// <param name="id">The id of the user to be updated</param>
        /// <param name="privacy">the view model</param>
        public void Put(int id, [FromBody] PrivacyViewModel privacy)
        {
            var user = this._userServices.GetById(id);
            privacy.SetUserPrivacySettings(ref user);
            this._userServices.UpdateUser(user);
        }
    }
}