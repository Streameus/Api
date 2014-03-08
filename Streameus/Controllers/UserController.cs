using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;

namespace Streameus.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        // GET api/user
        public IEnumerable<UserViewModel> Get()
        {
            var userList = new List<UserViewModel>();
            this._userServices.GetAll().ForEach(u => userList.Add(new UserViewModel(u)));
            if (!userList.Any())
                HttpErrors.NoResult("Empty Set");
            return userList;
        }

        // GET api/user/5
        public UserViewModel Get(int id)
        {
            var user = this._userServices.GetById(id);
            return new UserViewModel(user);
        }

        //TODO implementer le /me

        // POST api/user
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
                HttpErrors.Conflict(entryException.Message);
            }
            return new UserViewModel(newUser);
        }

        // PUT api/user/5
        public UserViewModel Put(int id, [FromBody] UserViewModel userViewModel)
        {
            var newUser = Mapper.Map<User>(userViewModel);
            try
            {
                this._userServices.UpdateUser(newUser);
            }
            catch (DuplicateEntryException entryException)
            {
                HttpErrors.Conflict(entryException.Message);
            }
            return new UserViewModel(newUser);
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
            this._userServices.Delete(id);
        }
    }
}