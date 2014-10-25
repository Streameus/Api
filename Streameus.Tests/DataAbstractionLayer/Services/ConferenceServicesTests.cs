using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataAbstractionLayer.Services;
using Streameus.DataBaseAccess;
using Streameus.Models;

namespace Streameus.Tests.DataAbstractionLayer.Services
{
    [TestClass()]
    public class ConferenceServicesTests
    {
        [TestMethod()]
        public void SuscribeUserToConferenceTest()
        {
            Assert.Inconclusive("Need to do more checkup");
            var unitOfWorkMocker = new UnitOfWorkMocker();
            var conferenceParametersServices = new Mock<IConferenceParametersServices>();
            var eventServices = new Mock<IEventServices>();
            var userServices = new Mock<IUserServices>();
            var roomServices = new Mock<IRoomServices>();
            var paymentServices = new Mock<IPaymentServices>();

            const int conferenceId = 41;
            const int userId = 42;
            var conference = new Conference() {Id = conferenceId};
            var user = new User() {Id = userId};

            unitOfWorkMocker.AddFakeDbSet(c => c.Conferences, new List<Conference>() {conference}.AsQueryable());
            userServices.Setup(f => f.GetById(It.IsAny<int>())).Returns(user);

            var conferenceServices = new ConferenceServices(unitOfWorkMocker.UnitOfWork,
                conferenceParametersServices.Object,
                eventServices.Object, userServices.Object, roomServices.Object, paymentServices.Object);
            conferenceServices.SuscribeUserToConference(conferenceId, userId);
            Assert.Equals(conference.Participants.Count, 1);
            Assert.Equals(conference.Participants.First().Id, userId);
        }

        [TestMethod()]
        public void UnsuscribeUserFromConferenceTest()
        {
            Assert.Inconclusive("Need to do more checkup");

            var unitOfWorkMocker = new UnitOfWorkMocker();

            var conferenceParametersServices = new Mock<IConferenceParametersServices>();
            var eventServices = new Mock<IEventServices>();
            var userServices = new Mock<IUserServices>();
            var roomServices = new Mock<IRoomServices>();
            var paymentServices = new Mock<IPaymentServices>();

            const int conferenceId = 41;
            const int userId = 42;
            var conference = new Conference() {Id = conferenceId};
            var user = new User() {Id = userId};

            unitOfWorkMocker.AddFakeDbSet(c => c.Conferences, new List<Conference>() {conference}.AsQueryable());
            userServices.Setup(f => f.GetById(It.IsAny<int>())).Returns(user);
            //Marche pas
            var conferenceServices = new ConferenceServices(unitOfWorkMocker.UnitOfWork,
                conferenceParametersServices.Object,
                eventServices.Object, userServices.Object, roomServices.Object, paymentServices.Object);
            conferenceServices.SuscribeUserToConference(conferenceId, userId);
            Assert.Equals(conference.Participants.Count, 1);
            Assert.Equals(conference.Participants.First().Id, userId);
        }
    }
}