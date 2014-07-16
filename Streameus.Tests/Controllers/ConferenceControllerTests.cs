using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using AutoFixture.AutoEF;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;
using Streameus.DataAbstractionLayer.Services;
using Xunit;
using Moq;
using Ploeh.AutoFixture;
using Streameus.Controllers;
using Streameus.DataAbstractionLayer;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Models;
using Streameus.ViewModels;
using Xunit.Extensions;

namespace Streameus.Tests.Controllers
{
    public class ConferenceControllerTests
    {
        [Theory, AutoEF]
        public void GetTest([Frozen] List<Conference> conferences,
            [Frozen] Mock<IConferenceServices> conferenceServices, ConferenceController controller)
        {
            //SETUP
            conferenceServices.Setup(s => s.GetAll()).Returns(conferences.AsQueryable);

            //Execute
            var confsVms = controller.Get();

            //Verify
            Assert.Equal(conferences.Count(), confsVms.Count());
            conferenceServices.Verify(s => s.GetAll(), Times.Once,
                "GetAll must be called exactly once!");
        }

        [Theory, AutoEF]
        public void GetWithIdTest([Frozen] Conference conference,
            [Frozen] Mock<IConferenceServices> conferenceServices, ConferenceController controller)
        {
            //SETUP
            var conferenceId = conference.Id;
            conferenceServices.Setup(s => s.GetById(It.Is((int i) => i == conferenceId))).Returns(conference);

            //Execute
            var confVm = controller.Get(conferenceId);

            //Verify
            Assert.Equal(confVm.Id, conferenceId);
            conferenceServices.Verify(s => s.GetById(It.IsAny<int>()), Times.Once,
                "GetById must be called exactly once!");
        }

        [Theory, AutoEF]
        public void PostTest(ConferenceFormViewModel conferenceVM, [Frozen] Conference conference,
            [Frozen] Mock<IConferenceCategoryServices> conferenceCategoryServiceMock,
            [Frozen] Mock<IConferenceServices> conferenceServiceMock,
            ConferenceController controller)
        {
            //SETUP
            //Le callback remplace la fonction addConference
            //On s'en sert pour simuler le save qui va donner son id a la conference.
            //Le "It" permet de specifier des conditions sur le parametre passe. 
            conferenceServiceMock.Setup(s => s.AddConference(It.IsAny<Conference>()))
                .Callback((Conference arg) => { arg.Id = conference.Id; });

            conferenceCategoryServiceMock.Setup(s => s.GetById(It.IsAny<int>())).Returns(conference.Category);

            //EXECUTE
            var newConf = controller.Post(conferenceVM);

            //VERIFY
            Assert.Equal(conference.Id, newConf.Id);
            //On verifie que la fonction AddConference a bien ete appelee une fois uniquement
            conferenceServiceMock.Verify(s => s.AddConference(It.IsAny<Conference>()), Times.Once(),
                "AddConference was called more than once or never called!");
        }

        [Theory, AutoEF]
        public void PutTest(ConferenceFormViewModel conferenceViewModel, [Frozen] Conference conference,
            [Frozen] Mock<IConferenceServices> conferenceServicesMock, ConferenceController controller)
        {
            //Setup
            conferenceServicesMock.Setup(s => s.GetById(It.Is((int i) => i == conferenceViewModel.Id)))
                .Returns(conference);

            //Execute
            var response = controller.Put(conferenceViewModel);

            //Verify
            Assert.NotNull(response);
            conferenceServicesMock.Verify(s => s.GetById(It.Is((int i) => i == conferenceViewModel.Id)), Times.Once);
            conferenceServicesMock.Verify(s => s.UpdateConference(It.IsAny<Conference>(), It.IsAny<int>()), Times.Once());
        }

        [Fact()]
        public void DeleteTest()
        {
        }
    }
}