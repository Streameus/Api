using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions.HttpErrors;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;

namespace Streameus.Controllers
{
    /// <summary>
    /// Event controller
    /// </summary>
    public class EventController : BaseController
    {
        private readonly IEventServices _eventServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="eventServices"></param>
        public EventController(IEventServices eventServices)
        {
            if (eventServices == null) throw new ArgumentNullException("evenServices");
            this._eventServices = eventServices;
        }

        // GET api/event
        /// <summary>
        /// Get all the events
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoResultException"></exception>
        public IEnumerable<EventViewModel> Get()
        {
            var eventList = new List<EventViewModel>();
            this._eventServices.GetAllWithIncludes().ForEach(e => eventList.Add(new EventViewModel(e)));
            if (!eventList.Any())
                throw new NoResultException("Empty Set");
            return eventList;
        }
    }
}
