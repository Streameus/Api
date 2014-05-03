using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Streameus.Controllers;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Models;
using Streameus.ViewModels;

namespace Streameus.Tests.Controllers
{
    [TestClass()]
    public class ConferenceControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            
        }

        [TestMethod()]
        public void GetWithIdTest()
        {
            
        }

        [TestMethod()]
        public void PostTest()
        {
            var conferenceServiceMock = new Mock<IConferenceServices>();
            var userServiceMock = new Mock<IUserServices>();

            Conference controllerGeneratedConference = null;
            var conference = new ConferenceFormViewModel()
            {
                Description = "test",
                Id = 45,
                Name = "testName",
                ScheduledDuration = 40,
                Time = DateTime.Now,
            };

            //J'utilise un callback pour mettre un id a la conference utilise dans le controller.
            //Sinon l'id est null est le Get pete.
            //Le "It" permet de specifier des conditions sur le parametre passe. 
            conferenceServiceMock.Setup(s => s.AddConference(It.IsAny<Conference>()))
                .Callback((Conference arg) =>
                {
                    controllerGeneratedConference = arg;
                    controllerGeneratedConference.Id = arg.Id = conference.Id;
                });
            userServiceMock.Setup(s => s.GetAll()).Returns(() => new List<User>() {new User()});
            //permet d'evaluer la variable plus tard, evite d'avoir null
            conferenceServiceMock.Setup(s => s.GetById(45)).Returns(() => controllerGeneratedConference);

            var controller = new ConferenceController(conferenceServiceMock.Object, userServiceMock.Object);

            //En vrai tester la valeur de retour ne sert a rien, puisqu'on teste le GET du coup...
            Assert.AreEqual(45, controller.Post(conference).Id);
        }

        [TestMethod()]
        public void PutTest()
        {
            
        }

        [TestMethod()]
        public void DeleteTest()
        {
           
        }
    }
}