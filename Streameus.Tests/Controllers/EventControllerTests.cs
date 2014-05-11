using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Streameus.Controllers;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Enums;
using Streameus.Models;
using Streameus.ViewModels;

namespace Streameus.Tests.Controllers
{
    [TestClass()]
    public class EventControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var eventServicesMock = new Mock<IEventServices>();
            var returnedList = this.GetDummyEventsList().OrderBy(ev => ev.Id);
            eventServicesMock.Setup(e => e.GetAllWithIncludes()).Returns(returnedList);
            var controller = new EventController(eventServicesMock.Object);
            var list = controller.Get();
            Assert.AreEqual(6, list.Count());
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var eventServicesMock = new Mock<IEventServices>();
            var controller = new EventController(eventServicesMock.Object);
            EventViewModel ev = controller.Get(1);
            Assert.AreEqual(1, ev.Id);
        }

        private IQueryable<Event> GetDummyEventsList()
        {
            var users = new List<User>
            {
                new User {Parameters = new Parameters(), FirstName = "Carson", LastName = "Alexander"},
                new User {Parameters = new Parameters(), FirstName = "Meredith", LastName = "Alonso"},
                new User {Parameters = new Parameters(), FirstName = "Arturo", LastName = "Anand", Gender = true}
            };
            users.ForEach(s =>
            {
                s.Pseudo = s.FullName;
                s.Email = s.FirstName + "." + s.LastName + "@epitech.eu";
            });
            var conference = new List<Conference>()
            {
                new Conference
                {
                    Owner = users[0],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Chemistry",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[1],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Microeconomics",
                    ScheduledDuration = 3,
                },
                new Conference
                {
                    Owner = users[2],
                    ConferenceParameters = new ConferenceParameters(),
                    Name = "Physics",
                    ScheduledDuration = 3,
                }
            };
            conference.ForEach(s =>
            {
                s.OwnerId = s.Owner.Id;
                s.Description = "Description de " + s.Name;
                s.Time = DateTime.Now;
            });
            return new List<Event>
            {
                new Event
                {
                    Author = users[1],
                    Type = DataBaseEnums.EventType.ParticipateConf,
                    Date = DateTime.Now,
                    EventItems = new List<EventItem>
                    {
                        new EventItem
                        {
                            Pos = 0,
                            TargetType = DataBaseEnums.EventItemType.User,
                            TargetId = users[1].Id,
                            Content = users[1].Pseudo
                        },
                        new EventItem
                        {
                            Pos = 1,
                            TargetType = DataBaseEnums.EventItemType.Conference,
                            TargetId = conference[1].Id,
                            Content = conference[1].Name
                        },
                    }
                },
                new Event
                {
                    Author = users[1],
                    Type = DataBaseEnums.EventType.CreateConf,
                    Date = DateTime.Now,
                    EventItems = new List<EventItem>
                    {
                        new EventItem
                        {
                            Pos = 0,
                            TargetType = DataBaseEnums.EventItemType.User,
                            TargetId = users[2].Id,
                            Content = users[2].Pseudo
                        },
                        new EventItem
                        {
                            Pos = 1,
                            TargetType = DataBaseEnums.EventItemType.Conference,
                            TargetId = conference[2].Id,
                            Content = conference[2].Name
                        },
                    }
                },
                new Event
                {
                    Author = users[1],
                    Type = DataBaseEnums.EventType.StartFollow,
                    Date = DateTime.Now,
                    EventItems = new List<EventItem>
                    {
                        new EventItem
                        {
                            Pos = 0,
                            TargetType = DataBaseEnums.EventItemType.User,
                            TargetId = users[2].Id,
                            Content = users[2].Pseudo
                        },
                        new EventItem
                        {
                            Pos = 1,
                            TargetType = DataBaseEnums.EventItemType.User,
                            TargetId = users[0].Id,
                            Content = users[0].Pseudo
                        },
                    }
                },
                new Event
                {
                    Author = users[2],
                    Type = DataBaseEnums.EventType.SuscribeConf,
                    Date = DateTime.Now,
                    EventItems = new List<EventItem>
                    {
                        new EventItem
                        {
                            Pos = 0,
                            TargetType = DataBaseEnums.EventItemType.User,
                            TargetId = users[0].Id,
                            Content = users[0].Pseudo
                        },
                        new EventItem
                        {
                            Pos = 1,
                            TargetType = DataBaseEnums.EventItemType.Conference,
                            TargetId = conference[0].Id,
                            Content = conference[0].Name
                        },
                    }
                },
                new Event
                {
                    Author = users[0],
                    Type = DataBaseEnums.EventType.CreateConf,
                    Date = DateTime.Now,
                    EventItems = new List<EventItem>
                    {
                        new EventItem
                        {
                            Pos = 0,
                            TargetType = DataBaseEnums.EventItemType.User,
                            TargetId = users[2].Id,
                            Content = users[2].Pseudo
                        },
                        new EventItem
                        {
                            Pos = 1,
                            TargetType = DataBaseEnums.EventItemType.Conference,
                            TargetId = conference[1].Id,
                            Content = conference[1].Name
                        },
                    }
                },
                new Event
                {
                    Author = users[0],
                    Type = DataBaseEnums.EventType.StartFollow,
                    Date = DateTime.Now,
                    EventItems = new List<EventItem>
                    {
                        new EventItem
                        {
                            Pos = 0,
                            TargetType = DataBaseEnums.EventItemType.User,
                            TargetId = users[2].Id,
                            Content = users[2].Pseudo
                        },
                        new EventItem
                        {
                            Pos = 1,
                            TargetType = DataBaseEnums.EventItemType.User,
                            TargetId = users[1].Id,
                            Content = users[1].Pseudo
                        },
                    }
                }
            }.AsQueryable();
        }
    }
}