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
        /// <param name="evt">Event to save</param>
        protected override void Save(Event evt)
        {
            if (evt.Id > 0)
                this.Update(evt);
            else
                this.Insert(evt);
            this.SaveChanges();
        }

        /// <summary>
        /// Add a new event in db
        /// </summary>
        /// <param name="evt">The event to be added</param>
        public void AddEvent(Event evt)
        {
            // TODO Definir la visibity de l'event par defaut si elle n'est pas set selon les params de l'user
            this.Save(evt);   
        }

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="evt">Event to be updated</param>
        public void UpdateEvent(Event evt)
        {
            this.Save(evt);
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