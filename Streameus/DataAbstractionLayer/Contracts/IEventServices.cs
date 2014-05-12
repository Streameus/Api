using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}