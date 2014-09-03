using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Streameus.App_GlobalResources;
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
    [RoutePrefix("api/User")]
    public class UserController : BaseController
    {
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        public UserController(IUserServices userServices)
        {
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._userServices = userServices;
        }

        // GET api/user
        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoResultException"></exception>
        public IEnumerable<UserViewModel> Get()
        {
            var userList = new List<UserViewModel>();
            this._userServices.GetAll().ForEach(u => userList.Add(new UserViewModel(u)));
            if (!userList.Any())
                throw new NoResultException("Empty Set");
            return userList;
        }

        // GET api/user/5
        /// <summary>
        /// Get one user
        /// </summary>
        /// <param name="id">the id of the user to get</param>
        /// <returns></returns>
        public UserViewModel Get(int id)
        {
            var user = this._userServices.GetById(id);
            return new UserViewModel(user);
        }

        // GET api/user/available
        /// <summary>
        /// Verify if a user pseudo is available 
        /// </summary>
        /// <param name="pseudo">the pseudo of the user to verify</param>
        /// <returns></returns>
        [Route("pseudoIsAvailable")]
        public Boolean GetAvailable(string pseudo)
        {
            return this._userServices.IsPseudoExist(pseudo);
        }

        // GET api/user/AmIFollowing/{id}
        /// <summary>
        /// Check if the user authenticated follows the specified user
        /// </summary>
        /// <param name="id">the id of the user to be checked</param>
        /// <returns></returns>
        [Authorize]
        [Route("AmIFollowing/{id}")]
        public Boolean GetAmI(int id)
        {
            var currentUserId = this.GetCurrentUserId();
            return this._userServices.IsUserFollowing(currentUserId, id);
        }

        /// <summary>
        /// Return the currently authenticated user
        /// </summary>
        /// <returns></returns>
        [Route("me")]
        [Authorize]
        public UserViewModel GetMe()
        {
            return Get(this.GetCurrentUserId());
        }

        // POST api/user
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        /// <exception cref="ConflictdException">An user already exist with same infos</exception>
        public UserViewModel Post([FromBody] UserViewModel userViewModel)
        {
            userViewModel.Id = 0;
            var newUser = Mapper.Map<User>(userViewModel);
            try
            {
                this._userServices.AddUser(newUser);
            }
            catch (DuplicateEntryException entryException)
            {
                throw new ConflictdException(entryException.Message);
            }
            return new UserViewModel(newUser);
        }

        // PUT api/user/5
        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        /// <exception cref="ConflictdException">An user already exist with same infos</exception>
        public UserViewModel Put(int id, [FromBody] UserViewModel userViewModel)
        {
            var newUser = Mapper.Map<User>(userViewModel);
            try
            {
                this._userServices.UpdateUser(newUser);
            }
            catch (DuplicateEntryException entryException)
            {
                throw new ConflictdException(entryException.Message);
            }
            return new UserViewModel(newUser);
        }

        // DELETE api/user/5
        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ForbiddenException"></exception>
        [Authorize]
        public void Delete(int id)
        {
            if (this.GetCurrentUserId() == id)
            {
                this._userServices.Delete(id);
            }
            else
            {
                throw new ForbiddenException(Translation.ForbiddenUserDelete);
            }
        }

        /// <summary>
        /// Get all conferences created by a specified user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [Route("{id}/conferences")]
        public IEnumerable<ConferenceViewModel> GetConferencesOfUser(int id)
        {
            var conferences = this._userServices.GetById(id).ConferencesCreated;
            var conferencesListe = new List<ConferenceViewModel>();
            conferences.ForEach(c => conferencesListe.Add(new ConferenceViewModel(c)));
            return conferencesListe;
        }

    }
}