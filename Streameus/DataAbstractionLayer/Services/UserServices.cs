using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Streameus.App_GlobalResources;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Enums;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    /// User services
    /// </summary>
    public class UserServices : BaseServices<User>, IUserServices
    {
        private readonly IParametersServices _parametersServices;
        private readonly IEventServices _eventServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="parametersServices"></param>
        /// <param name="eventServices"></param>
        public UserServices(IUnitOfWork unitOfWork, IParametersServices parametersServices, IEventServices eventServices) : base(unitOfWork)
        {
            this._parametersServices = parametersServices;
            this._eventServices = eventServices;
        }

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="user"></param>
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
                throw new DuplicateEntryException(Translation.UserWithSameEmailAlreadyExists);
            if (!this.IsUserPseudoUnique(newUser))
                throw new DuplicateEntryException("User with the same pseudo already exists");
            newUser.Parameters = new Parameters();
            this.Save(newUser);
        }

        /// <summary>
        /// Add a following to an user
        /// </summary>
        /// <param name="userId">The user's id who wants a following</param>
        /// <param name="userWantedId">The user's id who is followed</param>
        public bool AddFollowing(int userId, int userWantedId)
        {
            var user = this.GetById(userId);
            var userWanted = this.GetById(userWantedId);
            if (user.Abonnements.Contains(userWanted))
                return false;
            user.Abonnements.Add(userWanted);
            this.Save(user);
            this._eventServices.AddEvent(new Event
            {
                Author = user,
                Type = DataBaseEnums.EventType.StartFollow,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                    {
                        new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, TargetId = userId, Content = user.Pseudo},
                        new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.User, TargetId = userWantedId, Content = userWanted.Pseudo},
                    }
            });
            return true;
        }

        /// <summary>
        /// Remove a following of an user
        /// </summary>
        /// <param name="userId">The user's id who wants remove following</param>
        /// <param name="userUnwantedId">The user's id who is deleted</param>
        public bool RemoveFollowing(int userId, int userUnwantedId)
        {
            var user = this.GetById(userId);
            var userUnwanted = this.GetById(userUnwantedId);
            if (!user.Abonnements.Contains(userUnwanted))
                return false;
            user.Abonnements.Remove(userUnwanted);
            this.Save(user);
            return true;
        }

        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="DuplicateEntryException"></exception>
        public void UpdateUser(User user)
        {
            if (!this.IsUserEmailUnique(user))
                throw new DuplicateEntryException(Translation.UserWithSameEmailAlreadyExists);
            if (!this.IsUserPseudoUnique(user))
                throw new DuplicateEntryException("User with the same pseudo already exists");
            this.Save(user);
        }

        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        public new void Delete(int id)
        {
            var userToDelete = this.GetById(id);
            var parameters = userToDelete.Parameters;
            base.Delete(userToDelete);
            this._parametersServices.Delete(parameters);
            this.SaveChanges();
        }

        /// <summary>
        /// Returns all the user's followers
        /// </summary>
        /// <param name="id">userId</param>
        /// <exception cref="NoResultException">If user doesnt exists</exception>
        /// <exception cref="EmptyResultException">If user doesnt have any followers</exception>
        /// <returns>A list containing all the followers for the selected user</returns>
        public IQueryable<User> GetFollowersForUser(int id)
        {
            try
            {
                var followers = this.GetDbSet<User>().Single(u => u.Id == id).Followers.AsQueryable();
                if (!followers.Any())
                    throw new EmptyResultException("No followers");
                return followers;
            }
            catch (InvalidOperationException)
            {
                throw new NoResultException("No such user");
            }
        }

        /// <summary>
        /// Get all the abonnements for a user
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="EmptyResultException">No followings</exception>
        /// <exception cref="NoResultException">The user wasn't found</exception>
        /// <returns></returns>
        public IQueryable<User> GetAbonnementsForUser(int id)
        {
            try
            {
                var user = this.GetDbSet<User>().Single(u => u.Id == id);
                if (!user.AbonnementsVisibility)
                {
                    return Enumerable.Empty<User>().AsQueryable();
                }
                var abonnements = user.Abonnements.AsQueryable();
                if (!abonnements.Any())
                    throw new EmptyResultException("This user is not following anobody");
                return abonnements;
            }
            catch (InvalidOperationException)
            {
                throw new NoResultException("No such user");
            }
        }

        /// <summary>
        /// Check if the current user follows the target user
        /// </summary>
        /// <param name="currentUserId">The current user ID</param>
        /// <param name="targetUserId">the target user ID</param>
        /// <returns></returns>
        public bool IsUserFollowing(int currentUserId, int targetUserId)
        {
            var currentUser = this.GetById(currentUserId);
            return currentUser.Abonnements.Any(a => a.Id == targetUserId);
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
        /// Check if a pseudo exists in db
        /// </summary>
        /// <param name="pseudo">The pseudo to be checked</param>
        /// <returns>Returns true if the pseudo exists</returns>
        public bool IsPseudoExist(string pseudo)
        {
            return !this.GetDbSet<User>().Any(u => u.Pseudo == pseudo);
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