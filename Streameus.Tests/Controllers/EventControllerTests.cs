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
            var returnedList = this.GetDummyEventsList().OrderBy(ev => ev.Id);
            Event myEvent = returnedList.First();
            eventServicesMock.Setup(e => e.GetById(1)).Returns(myEvent);
            var controller = new EventController(eventServicesMock.Object);
            EventViewModel obj = controller.Get(1);
            Assert.AreEqual(1, obj.Id);
        }

        [TestMethod()]
        public void GetByAuthorIdTest()
        {
            const int id = 1;
            var eventServicesMock = new Mock<IEventServices>();
            var returnedList = this.GetDummyEventsList().OrderBy(ev => ev.Id).Where(ev => ev.AuthorId == id);
            eventServicesMock.Setup(e => e.GetEventsForUser(id)).Returns(returnedList);
            var controller = new EventController(eventServicesMock.Object);
            var list = controller.GetByAuthorId(id, null);
            foreach (var eventViewModel in list) {
                Assert.AreEqual(id, eventViewModel.AuthorId);
            }
        }

        private IQueryable<Event> GetDummyEventsList()
        {
            var users = new List<User>
            {
                new User {Id = 1, Parameters = new Parameters(), FirstName = "Carson", LastName = "Alexander"},
                new User {Id = 2, Parameters = new Parameters(), FirstName = "Meredith", LastName = "Alonso"},
                new User {Id = 3, Parameters = new Parameters(), FirstName = "Arturo", LastName = "Anand", Gender = true}
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
                    Id = 1,
                    Author = users[1], AuthorId = users[1].Id,
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
                    Id = 2,
                    Author = users[1], AuthorId = users[1].Id,
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
                    Id = 3,
                    Author = users[1], AuthorId = users[1].Id,
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
                    Id = 4,
                    Author = users[2], AuthorId = users[2].Id,
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
                    Id = 5,
                    Author = users[0], AuthorId = users[0].Id,
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
                    Id = 6,
                    Author = users[0], AuthorId = users[0].Id,
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