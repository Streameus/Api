using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Enums;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using NoResultException = Streameus.Exceptions.HttpErrors.NoResultException;

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
            // TODO Check que author et authorId ne sont pas vides ou s'assurer qu'il ont la meme value
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
            return this.GetDbSet<Event>().Include(e => e.EventItems).Where(ev => ev.Date < DateTime.Now);
        }

        /// <summary>
        /// Return all the events for the specified user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>A list of all the events</returns>
        /// <exception cref="EmptyResultException">If the user has no events</exception>
        /// <exception cref="NoResultException">If author doesnt exists</exception>
        public IQueryable<Event> GetEventsForUser(int userId)
        {
            try
            {
                var events = this.GetAllWithIncludes().Where(evt => evt.AuthorId == userId);
                // TODO Ajouter la traduction pour ce terme
                if (!events.Any())
                    throw new NoResultException("No events");
                return events;
            }
            catch (InvalidOperationException)
            {
                // TODO Ajouter la traduction pour ce terme
                throw new NoResultException("No such author");
            }
        }

        /// <summary>
        /// Create start following event
        /// </summary>
        /// <param name="user1">The user who wants a following</param>
        /// <param name="user2">The user who is followed</param>
        public void StartFollowing(User user1, User user2)
        {
            this.AddEvent(new Event
            {
                Author = user1,
                Type = DataBaseEnums.EventType.StartFollow,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                    {
                        new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, 
                            TargetId = user1.Id, Content = user1.Pseudo},
                        new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.User, 
                            TargetId = user2.Id, Content = user2.Pseudo},
                    }
            });
        }

        /// <summary>
        /// Create create conference event
        /// </summary>
        /// <param name="conf">The conference</param>
        public void CreateConf(Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = conf.Owner,
                Type = DataBaseEnums.EventType.CreateConf,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                    {
                        new EventItem {Pos = 0, TargetType = DataBaseEnums.EventItemType.User, 
                            TargetId = conf.Owner.Id, Content = conf.Owner.Pseudo},
                        new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Conference, 
                            TargetId = conf.Id, Content = conf.Name},
                    }
            });
        }
    }
}