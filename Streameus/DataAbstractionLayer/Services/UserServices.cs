using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    public class UserServices : BaseServices<User>, IUserServices
    {
        private readonly IParametersServices _parametersServices;

        public UserServices(IUnitOfWork unitOfWork, IParametersServices parametersServices) : base(unitOfWork)
        {
            this._parametersServices = parametersServices;
        }

        protected override void Save(User user)
        {
            if (user.Id > 0)
                this.Update(user);
            else
                this.Insert(user);
            this.SaveChanges();
        }

        /// <summary>
        /// Add a new user in db
        /// </summary>
        /// <param name="newUser">The user to be added</param>
        /// <exception cref="DuplicateEntryException">A user already exists with the same pseudo or email</exception>
        public void AddUser(User newUser)
        {
            if (!this.IsUserEmailUnique(newUser))
                throw new DuplicateEntryException("User with the same email already exists");
            if (!this.IsUserPseudoUnique(newUser))
                throw new DuplicateEntryException("User with the same pseudo already exists");
            newUser.Parameter = new Parameters();
            this.Save(newUser);
        }

        public void UpdateUser(User user)
        {
            if (!this.IsUserEmailUnique(user))
                throw new DuplicateEntryException("User with the same email already exists");
            if (!this.IsUserPseudoUnique(user))
                throw new DuplicateEntryException("User with the same pseudo already exists");
            this.Save(user);
        }

        public new void Delete(int id)
        {
            var userToDelete = this.GetById(id);
            var parameters = userToDelete.Parameter;
            base.Delete(userToDelete);
            this._parametersServices.Delete(parameters);
            this.SaveChanges();
        }

        /// <summary>
        /// Returns all the user's followers
        /// </summary>
        /// <param name="id">userId</param>
        /// <exception cref="NoResultException">If user doesnt exists</exception>
        /// <returns>A list containing all the followers for the selected user</returns>
        public IQueryable<User> GetFollowersForUser(int id)
        {
            try
            {
                var followers = this.GetDbSet<User>().Single(u => u.Id == id).Followers.AsQueryable();
                return followers;
            }
            catch (InvalidOperationException e)
            {
                throw new NoResultException("No such user");
            }
        }

        public IQueryable<User> GetAbonnementsForUser(int id)
        {
            try
            {
                var user = this.GetDbSet<User>().Single(u => u.Id == id);
                if (user.AbonnementsVisibility)
                {
                    var abonnements = user.Followers.AsQueryable();
                    return abonnements;
                }
                return Enumerable.Empty<User>().AsQueryable();
            }
            catch (InvalidOperationException e)
            {
                throw new NoResultException("No such user");
            }
        }


        /// <summary>
        /// Check if user pseudo exists in db
        /// </summary>
        /// <param name="user">The user to be checked</param>
        /// <returns>Returns true if the pseudo doesnt exists</returns>
        private bool IsUserPseudoUnique(User user)
        {
            return !this.GetDbSet<User>().Any(u => u.Pseudo == user.Pseudo && u.Id != user.Id);
        }

        /// <summary>
        /// Check if user email exists in db
        /// </summary>
        /// <param name="user">The user to be checked</param>
        /// <returns>Returns true if the email doesnt exists</returns>
        public bool IsUserEmailUnique(User user)
        {
            return !this.GetDbSet<User>().Any(u => u.Email == user.Email && u.Id != user.Id);
        }
    }
}