using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// The interface for the event services
    /// </summary>
    public interface IEventServices : IBaseServices<Event>
    {
        /// <summary>
        /// Add a new event in db
        /// </summary>
        /// <param name="evt">The event to be added</param>
        void AddEvent(Event evt);

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="evt">Event to be updated</param>
        void UpdateEvent(Event evt);

        /// <summary>
        /// get events with event items
        /// </summary>
        /// <returns></returns>
        IQueryable<Event> GetAllWithIncludes();

        /// <summary>
        /// Return all the events for the specified user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>A list of all the events</returns>
        /// <exception cref="EmptyResultException">If the user has no events</exception>
        /// <exception cref="NoResultException">If author doesnt exists</exception>
        IQueryable<Event> GetEventsForUser(int userId);

        /// <summary>
        /// Create start following event
        /// </summary>
        /// <param name="user1">The user who wants a following</param>
        /// <param name="user2">The user who is followed</param>
        void StartFollowing(User user1, User user2);

        /// <summary>
        /// Create create conference event
        /// </summary>
        /// <param name="conf">The conference</param>
        void CreateConf(Conference conf);
    }
}