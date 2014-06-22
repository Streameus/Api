using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Streameus.App_GlobalResources;
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
            //no check needed for the Author. Entity does it for us ;)
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
                if (!events.Any())
                    throw new NoResultException(Translation.NoResultExceptionEvent);
                return events;
            }
            catch (InvalidOperationException)
            {
                throw new NoResultException(Translation.NoResultExceptionAuthor);
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
                Visibility = user1.AbonnementsVisibility,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user1.Id,
                        Content = user1.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user2.Id,
                        Content = user2.Pseudo
                    },
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
                Visibility = true,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Owner.Id,
                        Content = conf.Owner.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                }
            });
        }


        /// <summary>
        /// Create bilan create conf month event
        /// </summary>
        /// <param name="user">The user who created the conferences</param>
        /// <param name="nbConf">The number of conference created by the user this month</param>
        public void BilanCreateConfMonth(User user, int nbConf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Type = DataBaseEnums.EventType.BilanCreateConfMonth,
                Date = DateTime.Now,
                Visibility = true,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Int, Content = nbConf.ToString()},
                }
            });
        }

        /// <summary>
        /// Create BilanCreateConfWeek event
        /// </summary>
        /// <param name="user">The user who created the conferences</param>
        /// <param name="nbConf">The number of conference created by the user this week</param>
        public void BilanCreateConfWeek(User user, int nbConf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.BilanCreateConfWeek,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Int, Content = nbConf.ToString()},
                }
            });
        }

        /// <summary>
        /// Create BilanFollowers event
        /// </summary>
        /// <param name="user">The user who has followers</param>
        public void BilanFollowers(User user)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.BilanFollowers,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Int,
                        Content = user.Followers.Count.ToString()
                    },
                }
            });
        }

        /// <summary>
        /// Create BilanFollowingMonth event
        /// </summary>
        /// <param name="user">The user who follows people</param>
        /// <param name="nbFollowing">number of people who have been following during the month</param>
        public void BilanFollowingMonth(User user, int nbFollowing)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = user.AbonnementsVisibility,
                Type = DataBaseEnums.EventType.BilanFollowingMonth,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Int,
                        Content = nbFollowing.ToString()
                    },
                }
            });
        }

        /// <summary>
        /// Create BilanFollowingWeek event
        /// </summary>
        /// <param name="user">The user who follows people</param>
        /// <param name="nbFollowing">number of people who have been following during the week</param>
        public void BilanFollowingWeek(User user, int nbFollowing)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = user.AbonnementsVisibility,
                Type = DataBaseEnums.EventType.BilanFollowingWeek,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Int,
                        Content = nbFollowing.ToString()
                    },
                }
            });
        }

        /// <summary>
        /// Create BilanParticipateConfMonth event
        /// </summary>
        /// <param name="user">The user who participates to the conferences</param>
        /// <param name="nbConf">The number of conferences the user has participated this month</param>
        public void BilanParticipateConfMonth(User user, int nbConf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.BilanParticipateConfMonth,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Int, Content = nbConf.ToString()},
                }
            });
        }

        /// <summary>
        /// Create BilanParticipateConfWeek event
        /// </summary>
        /// <param name="user">The user who participates to the conferences</param>
        /// <param name="nbConf">The number of conferences the user has participated this week</param>
        public void BilanParticipateConfWeek(User user, int nbConf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.BilanParticipateConfMonth,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem {Pos = 1, TargetType = DataBaseEnums.EventItemType.Int, Content = nbConf.ToString()},
                }
            });
        }

        /// <summary>
        /// Create BilanReputation event
        /// </summary>
        /// <param name="user">A user to display reputation</param>
        public void BilanReputation(User user)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.BilanReputation,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Int,
                        Content = user.Reputation.ToString()
                    },
                }
            });
        }

        /// <summary>
        /// Create ChangePhoto event
        /// </summary>
        /// <param name="user">The user who changed his photo</param>
        public void ChangePhoto(User user)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.ChangePhoto,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                }
            });
        }

        /// <summary>
        /// Create ConfDate event
        /// </summary>
        /// <param name="conf">The conference which has been modified (date)</param>
        public void ConfDate(Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = conf.Owner,
                Visibility = true,
                Type = DataBaseEnums.EventType.ConfDate,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Owner.Id,
                        Content = conf.Owner.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                    new EventItem
                    {
                        Pos = 2,
                        TargetType = DataBaseEnums.EventItemType.DateTime,
                        TargetId = conf.Id,
                        Content = conf.Time.ToString()
                    },
                }
            });
        }

        /// <summary>
        /// Create ConfDescription event
        /// </summary>
        /// <param name="conf">The conference which has been modified (description)</param>
        public void ConfDescription(Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = conf.Owner,
                Visibility = true,
                Type = DataBaseEnums.EventType.ConfDescription,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Owner.Id,
                        Content = conf.Owner.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                }
            });
        }

        /// <summary>
        /// Create ConferenceEnd event
        /// </summary>
        /// <param name="conf">The conference is ended</param>
        public void ConferenceEnd(Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = conf.Owner,
                Visibility = true,
                Type = DataBaseEnums.EventType.ConferenceEnd,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Owner.Id,
                        Content = conf.Owner.Pseudo
                    },
                }
            });
        }

        /// <summary>
        /// Create ConferenceStart event
        /// </summary>
        /// <param name="conf">The conference is beginning</param>
        public void ConferenceStart(Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = conf.Owner,
                Visibility = true,
                Type = DataBaseEnums.EventType.ConferenceEnd,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Owner.Id,
                        Content = conf.Owner.Pseudo
                    },
                }
            });
        }

        /// <summary>
        /// Create ConfTime event
        /// </summary>
        /// <param name="user">The user who modified the conference</param>
        /// <param name="conf">The conference which has been modified (time)</param>
        public void ConfTime(User user, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = conf.Owner,
                Visibility = true,
                Type = DataBaseEnums.EventType.ConfTime,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Owner.Id,
                        Content = conf.Owner.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                    new EventItem
                    {
                        Pos = 2,
                        TargetType = DataBaseEnums.EventItemType.DateTime,
                        TargetId = conf.Id,
                        Content = conf.Time.Hour.ToString()
                    },
                }
            });
        }

        /// <summary>
        /// Create ConfTitle event
        /// </summary>
        /// <param name="user">The user who modified the conference</param>
        /// <param name="conf">The conference which has been modified (title)</param>
        public void ConfTitle(User user, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = conf.Owner,
                Visibility = true,
                Type = DataBaseEnums.EventType.ConfTitle,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Owner.Id,
                        Content = conf.Owner.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                }
            });
        }

        /// <summary>
        /// Create FirstFollower event
        /// </summary>
        /// <param name="user1">The user who is followed</param>
        /// <param name="user2">The user who is following</param>
        public void FirstFollower(User user1, User user2)
        {
            this.AddEvent(new Event
            {
                Author = user1,
                Visibility = user2.AbonnementsVisibility,
                Type = DataBaseEnums.EventType.FirstFollower,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user1.Id,
                        Content = user1.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user2.Id,
                        Content = user2.Pseudo
                    },
                }
            });
        }

        /// <summary>
        /// Create ParticipateConf event
        /// </summary>
        /// <param name="user">The user who participated to the conference</param>
        /// <param name="conf">The conference</param>
        public void ParticipateConf(User user, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.FirstFollower,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                }
            });
        }

        /// <summary>
        /// Create RateConf event
        /// </summary>
        /// <param name="user">The user who is rating the conference</param>
        /// <param name="stars">Number of stars</param>
        /// <param name="conf">The conference which is rated</param>
        public void RateConf(User user, int stars, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.RateConf,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Int,
                        TargetId = conf.Id,
                        Content = stars.ToString()
                    },
                    new EventItem
                    {
                        Pos = 2,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                }
            });
        }

        /// <summary>
        /// Create Recommand event
        /// </summary>
        /// <param name="user">The user who recommands the conference</param>
        /// <param name="conf">The conference which is recommanded</param>
        public void Recommand(User user, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.Recommand,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                }
            });
        }

        /// <summary>
        /// Create ShareConf event
        /// </summary>
        /// <param name="user">The user who shared the conference</param>
        /// <param name="conf">The conference which is shared</param>
        public void ShareConf(User user, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.ShareConf,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                    new EventItem
                    {
                        Pos = 2,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.OwnerId,
                        Content = conf.Owner.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 3,
                        TargetType = DataBaseEnums.EventItemType.DateTime,
                        TargetId = conf.Id,
                        Content = conf.Time.ToString()
                    },
                }
            });
        }

        /// <summary>
        /// Create SuscribeConf event
        /// </summary>
        /// <param name="user">The user who participates at the conference</param>
        /// <param name="conf">The conference</param>
        public void SuscribeConf(User user, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.SuscribeConf,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                }
            });
        }

        /// <summary>
        /// Create UpdateMsgPerso event
        /// </summary>
        /// <param name="user">The user who modified his personnal message</param>
        public void UpdateMsgPerso(User user)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.UpdateMsgPerso,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.String,
                        TargetId = user.Id,
                        Content = user.Description
                    },
                }
            });
        }

        /// <summary>
        /// Create UserInvitation event
        /// </summary>
        /// <param name="user">The user who made the invitation</param>
        /// <param name="conf">The conference</param>
        public void UserInvitation(User user, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.UpdateMsgPerso,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                    new EventItem
                    {
                        Pos = 2,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = conf.Owner.Id,
                        Content = conf.Owner.Pseudo
                    },
                }
            });
        }

        /// <summary>
        /// Create WillParticipateConf event
        /// </summary>
        /// <param name="user">The user who will participate to the conference</param>
        /// <param name="conf">The conference</param>
        public void WillParticipateConf(User user, Conference conf)
        {
            this.AddEvent(new Event
            {
                Author = user,
                Visibility = true,
                Type = DataBaseEnums.EventType.WillParticipateConf,
                Date = DateTime.Now,
                EventItems = new List<EventItem>
                {
                    new EventItem
                    {
                        Pos = 0,
                        TargetType = DataBaseEnums.EventItemType.User,
                        TargetId = user.Id,
                        Content = user.Pseudo
                    },
                    new EventItem
                    {
                        Pos = 1,
                        TargetType = DataBaseEnums.EventItemType.Conference,
                        TargetId = conf.Id,
                        Content = conf.Name
                    },
                    new EventItem
                    {
                        Pos = 2,
                        TargetType = DataBaseEnums.EventItemType.DateTime,
                        TargetId = conf.Id,
                        Content = conf.Time.Date.ToString()
                    },
                    new EventItem
                    {
                        Pos = 3,
                        TargetType = DataBaseEnums.EventItemType.DateTime,
                        TargetId = conf.Id,
                        Content = conf.Time.Hour.ToString()
                    },
                }
            });
        }
    }
}