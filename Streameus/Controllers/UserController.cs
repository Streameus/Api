﻿using System;
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
    /// User controller
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        public UserController(IUserServices userServices)
        {
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

        //TODO implementer le /me

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

        //todo seul l'user peut se supprimer
        // DELETE api/user/5
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            this._userServices.Delete(id);
        }
    }
}