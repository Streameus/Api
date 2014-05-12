using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    /// Event services
    /// </summary>
    public class EventServices : BaseServices<Event>, IEventServices
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public EventServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Save event
        /// </summary>
        /// <param name="obj"></param>
        protected override void Save(Event obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get events with event items
        /// </summary>
        /// <returns></returns>
        public IQueryable<Event> GetAllWithIncludes()
        {
            return this.GetDbSet<Event>().Include(e => e.EventItems);
        }
    }
}