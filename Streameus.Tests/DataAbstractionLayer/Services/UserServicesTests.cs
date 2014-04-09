using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataAbstractionLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Streameus.DataBaseAccess;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services.Tests
{
    [TestClass()]
    public class UserServicesTests
    {
        [TestMethod()]
        public void GetAbonnementsForUserTest()
        {
        }

        [TestMethod()]
        public void UserServicesTest()
        {
        }

        [TestMethod()]
        public void AddUserTest()
        {
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
        }

        [TestMethod()]
        public void DeleteTest()
        {
        }

        [TestMethod()]
        public void GetFollowersForUserTest()
        {
        }

        /// <summary>
        /// No need to test for the User not found, because the BaseServices Handles it for you
        /// </summary>
        [TestMethod()]
        public void IsUserFollowingTest()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var parameterServices = new Mock<IParametersServices>();
            //Le user qui est followed
            var targetUser = new User()
            {
                Id = 21
            };
            //On cree notre currentUser et on lui ajoute un abonnement au target user
            var currentUser = new User()
            {
                Id = 42,
                Abonnements = new Collection<User>()
                {
                    targetUser
                }
            };

            //Partie compliquee, on simule le retour du Find, du DbSet qui est lui meme retourne par unitOfWork
            unitOfWork.Setup(f => f.GetDbSet<User>()).Returns(new Func<DbSet<User>>(() =>
            {
                var dbSetMock = new Mock<DbSet<User>>();
                dbSetMock.Setup(f => f.Find(It.Is<int>(id => id == 42))).Returns(currentUser);
                return dbSetMock.Object;
            })
                );
            var userServices = new UserServices(unitOfWork.Object, parameterServices.Object);
            //On appelle la methode, on check que ca "marche" dans les 2 cas
            Assert.AreEqual(userServices.IsUserFollowing(currentUser.Id, targetUser.Id), true);
            Assert.AreEqual(userServices.IsUserFollowing(currentUser.Id, 304), false);
        }

        [TestMethod()]
        public void IsUserEmailUniqueTest()
        {
        }
    }
}