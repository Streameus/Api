using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    public interface IUserServices : IBaseServices<User>
    {
        void AddUser(User newUser);
        void UpdateUser(User user);
        void Delete(int id);

        /// <summary>
        /// Returns all the user's followers
        /// </summary>
        /// <param name="id">userId</param>
        /// <exception cref="NoResultException">If user doesnt exists</exception>
        /// <returns>A list containing all the followers for the selected user</returns>
        IQueryable<User> GetFollowersForUser(int id);

        IQueryable<User> GetAbonnementsForUser(int id);
    }
}