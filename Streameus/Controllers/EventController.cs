using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;

namespace Streameus.Controllers
{
    /// <summary>
    /// Event controller
    /// </summary>
    [RoutePrefix("api/Event")]
    public class EventController : BaseController
    {
        private readonly IEventServices _eventServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="eventServices"></param>
        public EventController(IEventServices eventServices)
        {
            if (eventServices == null)
                throw new ArgumentNullException("evenServices");
            this._eventServices = eventServices;
        }

        // GET api/event
        /// <summary>
        /// Get all the events
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoResultException"></exception>
        [Authorize]
        public IEnumerable<EventViewModel> Get()
        {
            var eventList = new List<EventViewModel>();
            this._eventServices.GetAllWithIncludes().ForEach(e => eventList.Add(new EventViewModel(e)));
            if (!eventList.Any())
                throw new NoResultException("Empty Set");
            return eventList;
        }

        /// GET api/event/{id}
        /// <summary>
        /// Get one event
        /// </summary>
        /// <param name="id">the id of the event to get</param>
        /// <returns></returns>
        [Authorize]
        public EventViewModel Get(int id)
        {
            var _event = this._eventServices.GetById(id);
            return new EventViewModel(_event);
        }

        /// GET api/event/author/{id}
        /// <summary>
        /// Get all event for a specific author
        /// </summary>
        /// <param name="id">Author Id</param>
        /// <param name="options">Odata options</param>
        /// <returns></returns>
        /// <exception cref="NoResultException">No results</exception>
        [Authorize]
        [Route("author/{id}")]
        public IEnumerable<EventViewModel> GetByAuthorId(int id, ODataQueryOptions<Event> options = null)
        {
            var eventList = new List<EventViewModel>();
            var events = this._eventServices.GetEventsForUser(id);
            if (options != null)
                events = options.ApplyTo(events) as IQueryable<Event>;
            events.ForEach(e => eventList.Add(new EventViewModel(e)));
            if (!eventList.Any())
                throw new Exceptions.HttpErrors.NoResultException("Empty Set");
            return eventList;
        }
    }
}