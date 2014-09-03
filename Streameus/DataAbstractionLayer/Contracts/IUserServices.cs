using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// The interface for the user services
    /// </summary>
    public interface IUserServices : IBaseServices<User>
    {
        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="newUser">the user to be added</param>
        /// <exception cref="DuplicateEntryException">A user already exists with the same pseudo or email</exception>
        void AddUser(User newUser);

        /// <summary>
        /// Add a following to an user
        /// </summary>
        /// <param name="userId">The user's id who wants a following</param>
        /// <param name="userWantedId">The user's id who is followed</param>
        bool AddFollowing(int userId, int userWantedId);

        /// <summary>
        /// Remove a following of an user
        /// </summary>
        /// <param name="userId">The user's id who wants remove following</param>
        /// <param name="userUnwantedId">The user's id who is deleted</param>
        bool RemoveFollowing(int userId, int userUnwantedId);

        /// <summary>
        /// Update a new user
        /// </summary>
        /// <param name="user">The user to be updated</param>
        /// <exception cref="DuplicateEntryException"></exception>
        void UpdateUser(User user);

        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        void Delete(int id);

        /// <summary>
        /// Check if a pseudo exists in db
        /// </summary>
        /// <param name="pseudo">The pseudo to be checked</param>
        /// <returns>Returns true if the pseudo exists</returns>
        bool IsPseudoExist(string pseudo);

        /// <summary>
        /// Returns all the user's followers
        /// </summary>
        /// <param name="id">userId</param>
        /// <exception cref="NoResultException">If user doesnt exists</exception>
        /// <exception cref="EmptyResultException">If user doesnt have any followers</exception>
        /// <returns>A list containing all the followers for the selected user</returns>
        IQueryable<User> GetFollowersForUser(int id);

        /// <summary>
        /// Get all the abonnements of an user
        /// </summary>
        /// <param name="id">the user id</param>
        /// <returns>a list of users</returns>
        /// <exception cref="EmptyResultException">No followings</exception>
        /// <exception cref="NoResultException">The user wasn't found</exception>
        IQueryable<User> GetAbonnementsForUser(int id);

        /// <summary>
        /// Check if the current user follows the target user
        /// </summary>
        /// <param name="currentUserId">The current user ID</param>
        /// <param name="targetUserId">the target user ID</param>
        /// <returns></returns>
        bool IsUserFollowing(int currentUserId, int targetUserId);

        /// <summary>
        /// Get suggested users to follow based on a user
        /// </summary>
        /// <param name="userId">The Id of the user needing suggestions</param>
        /// <param name="take">The max number of results desired</param>
        /// <returns>The list of suggested users</returns>
        IEnumerable<User> GetSuggestionsForUser(int userId, int take = 5);

        /// <summary>
        /// Get the 5 users with the highest rep
        /// </summary>
        /// <returns></returns>
        /// <param name="take">The max number of results desired</param>
        IQueryable<User> GetUsersWithBestReputation(int take = 5);

        /// <summary>
        /// Check if the current user is registered to the target conf
        /// </summary>
        /// <param name="currentUserId">The current user ID</param>
        /// <param name="targetConfId">The target conf ID</param>
        /// <returns></returns>
        bool IsUserRegistered(int currentUserId, int targetConfId);
    }
}