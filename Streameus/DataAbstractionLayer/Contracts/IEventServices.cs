using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// The interface for the user services
    /// </summary>
    public interface IEventServices : IBaseServices<Event>
    {
        /// <summary>
        /// get events with event items
        /// </summary>
        /// <returns></returns>
        IQueryable<Event> GetAllWithIncludes();
    }
}