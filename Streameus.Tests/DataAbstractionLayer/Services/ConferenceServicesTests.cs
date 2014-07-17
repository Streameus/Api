using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Moq;
using Ploeh.AutoFixture.Xunit;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataAbstractionLayer.Services;
using Streameus.DataBaseAccess;
using Streameus.Enums;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using Xunit;
using Xunit.Extensions;

namespace Streameus.Tests.DataAbstractionLayer.Services
{
    public class ConferenceServicesTests
    {
        [Theory, AutoEF]
        public void SuscribeUserToConferenceTest([Frozen] Conference conference,
            [Frozen] Mock<IDbSet<Conference>> confList,
            User user,
            [Frozen] Mock<IUnitOfWork> unitOfWork,
            [Frozen] Mock<IUserServices> userServices,
            ConferenceServices conferenceServices)
        {
            //SETUP
            //On regle le find pour retourner notre conference
            confList.Setup(s => s.Find(It.Is((int i) => i == conference.Id))).Returns(conference);
            // on regle le getDbset pour retourner notre liste (celle qui retourne notre conf)
            unitOfWork.Setup(s => s.GetDbSet<Conference>()).Returns(confList.Object);
            //On retourne notre User
            userServices.Setup(f => f.GetById(It.Is((int i) => i == user.Id))).Returns(user);
            // On save le nombre de registred initial (il est variable)
            var originalRegisteredCount = conference.Registred.Count;

            //Execute
            conferenceServices.SuscribeUserToConference(conference.Id, user.Id);

            //Verify
            unitOfWork.Verify(s => s.SaveChanges(), Times.Once, "Looks like the datas are not saved!");
            //On verifie que on a bien un registred de plus
            Assert.Equal(originalRegisteredCount + 1, conference.Registred.Count);
            //On verifie que c'est bien notre user
            Assert.True(conference.Registred.Any(u => u.Id == user.Id));
        }

        [Theory, AutoEF]
        public void SuscribeUserToConferenceTest_InThePast([Frozen] Conference conference,
            [Frozen] Mock<IDbSet<Conference>> confList,
            User user,
            [Frozen] Mock<IUnitOfWork> unitOfWork,
            [Frozen] Mock<IUserServices> userServices,
            ConferenceServices conferenceServices)
        {
            //SETUP
            conference.Time = DateTime.Now - TimeSpan.FromDays(conference.Id /*Random value*/);

            //On genere un status aleatoire en evitant en cours (car dans ce cas la ca marcherait)
            conference.Status = GetRandomConfStatus(randomSeed: conference.Id, avoid: DataBaseEnums.ConfStatus.EnCours);

            //On regle le find pour retourner notre conference
            confList.Setup(s => s.Find(It.Is((int i) => i == conference.Id))).Returns(conference);
            // on regle le getDbset pour retourner notre liste (celle qui retourne notre conf)
            unitOfWork.Setup(s => s.GetDbSet<Conference>()).Returns(confList.Object);
            //On retourne notre User
            userServices.Setup(f => f.GetById(It.Is((int i) => i == user.Id))).Returns(user);
            // On save le nombre de registred initial (il est variable)
            var originalRegisteredCount = conference.Registred.Count;

            //Execute
            Assert.Throws<ForbiddenException>(() => conferenceServices.SuscribeUserToConference(conference.Id, user.Id));

            //Verify
            unitOfWork.Verify(s => s.SaveChanges(), Times.Never, "Looks like the datas are saved, they shouldn't be!");
            //On verifie que on a bien un registred de plus
            Assert.Equal(originalRegisteredCount, conference.Registred.Count);
            //On verifie que c'est bien notre user
            Assert.False(conference.Registred.Any(u => u.Id == user.Id));
        }

        [Theory, AutoEF]
        public void UnsuscribeUserFromConferenceTest([Frozen] Conference conference,
            [Frozen] Mock<IDbSet<Conference>> confList, [Frozen] Mock<IUserServices> userServices,
            [Frozen] Mock<IUnitOfWork> unitOfWork, ConferenceServices conferenceServices)
        {
            //Setup
            conference.Time = DateTime.Now + TimeSpan.FromDays(conference.Id /*Nombre random*/);
            confList.Setup(s => s.Find(It.Is((int i) => i == conference.Id))).Returns(conference);
            unitOfWork.Setup(s => s.GetDbSet<Conference>()).Returns(confList.Object);
            var user = conference.Registred.First();
            userServices.Setup(f => f.GetById(It.Is((int i) => i == user.Id))).Returns(user);
            var originalRegisteredCount = conference.Registred.Count;


            //Execute
            conferenceServices.UnsuscribeUserFromConference(conference.Id, user.Id);

            //Verify
            unitOfWork.Verify(s => s.SaveChanges(), Times.Once, "Looks like the datas are not saved!");
            Assert.Equal(originalRegisteredCount - 1, conference.Registred.Count);
            Assert.False(conference.Registred.Any(u => u.Id == user.Id));
        }

        [Theory, AutoEF]
        public void UnsuscribeUserFromConferenceTest_InThePast([Frozen] Conference conference,
            [Frozen] Mock<IDbSet<Conference>> confList, [Frozen] Mock<IUserServices> userServices,
            [Frozen] Mock<IUnitOfWork> unitOfWork, ConferenceServices conferenceServices)
        {
            //Setup
            conference.Time = DateTime.Now - TimeSpan.FromDays(conference.Id /*Nombre random*/);
            confList.Setup(s => s.Find(It.Is((int i) => i == conference.Id))).Returns(conference);
            unitOfWork.Setup(s => s.GetDbSet<Conference>()).Returns(confList.Object);
            var user = conference.Registred.First();
            userServices.Setup(f => f.GetById(It.Is((int i) => i == user.Id))).Returns(user);
            var originalRegisteredCount = conference.Registred.Count;


            //Execute
            Assert.Throws<ForbiddenException>(
                () => conferenceServices.UnsuscribeUserFromConference(conference.Id, user.Id));

            //Verify
            unitOfWork.Verify(s => s.SaveChanges(), Times.Never, "Looks like the datas are saved, they shouldn't be!");
            Assert.Equal(originalRegisteredCount, conference.Registred.Count);
            Assert.True(conference.Registred.Any(u => u.Id == user.Id));
        }


        /// <summary>
        /// Get a random Conf Status.
        /// </summary>
        /// <param name="randomSeed">a positive int > 0</param>
        /// <param name="avoid">Optionnal parameter to avoid one of the values</param>
        /// <returns></returns>
        private static DataBaseEnums.ConfStatus GetRandomConfStatus(int randomSeed,
            DataBaseEnums.ConfStatus? avoid = null)
        {
            var list = new List<DataBaseEnums.ConfStatus>()
            {
                DataBaseEnums.ConfStatus.AVenir,
                DataBaseEnums.ConfStatus.EnCours,
                DataBaseEnums.ConfStatus.Finie
            };
            if (avoid.HasValue)
                list.Remove(avoid.Value);
            var index = randomSeed%list.Count - 1;
            index = index >= 0 ? index : 0;
            return list[index];
        }
    }
}