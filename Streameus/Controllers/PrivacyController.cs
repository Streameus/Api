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
    public class PrivacyController : BaseController
    {
        private readonly IUserServices _userServices;

        public PrivacyController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        // GET api/privacy
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/privacy/5
        public PrivacyViewModel Get(int id)
        {
            return new PrivacyViewModel(this._userServices.GetById(id));
        }

        // Put api/privacy
        public void Put(int id, [FromBody] PrivacyViewModel privacy)
        {
            if (!ModelState.IsValid)
                HttpErrors.ValidationError(ModelState);
            var user = this._userServices.GetById(id);
            privacy.SetUserPrivacySettings(ref user);
            this._userServices.UpdateUser(user);
        }
    }
}