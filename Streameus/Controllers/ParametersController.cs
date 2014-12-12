using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
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
    /// Parameters controller
    /// </summary>
    [RoutePrefix("api/Parameters")]
    public class ParametersController : BaseController
    {
        private readonly IParametersServices _parametersServices;
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parametersServices"></param>
        /// <param name="userServices"></param>
        public ParametersController(IParametersServices parametersServices, IUserServices userServices)
        {
            this._parametersServices = parametersServices;
            this._userServices = userServices;
        }

        // GET api/parameters/5
        /// <summary>
        /// Get user's parameters.
        /// </summary>
        /// <param name="id">the id of the user to get the parameters</param>
        /// <returns></returns>
        [Authorize]
        public ParametersViewModel Get(int id)
        {
            var user = this._userServices.GetById(id);
            return new ParametersViewModel(user);
        }

        // POST api/parameters
        /// <summary>
        /// Create parameters for an user
        /// </summary>
        /// <param name="parametersViewModel"></param>
        /// <param name="id">id of user</param>
        /// <returns></returns>
        [Authorize]
        public void Post(int id, [FromBody] ParametersViewModel parametersViewModel)
        {
            var user = this._userServices.GetById(id);
            parametersViewModel.SetUserParameters(ref user);
            this._userServices.UpdateUser(user);
        }
    }
}