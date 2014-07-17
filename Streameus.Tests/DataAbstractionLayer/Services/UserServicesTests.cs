using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Ploeh.AutoFixture.Xunit;
using Streameus.DataBaseAccess;
using Xunit;
using Xunit.Extensions;
using Moq;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataAbstractionLayer.Services;
using Streameus.Models;

namespace Streameus.Tests.DataAbstractionLayer.Services
{
    public class UserServicesTests
    {
        [Fact()]
        public void GetAbonnementsForUserTest()
        {
        }

        [Fact()]
        public void UserServicesTest()
        {
        }

        [Fact()]
        public void AddUserTest()
        {
        }

        [Fact()]
        public void UpdateUserTest()
        {
        }

        [Fact()]
        public void DeleteTest()
        {
        }

        [Fact()]
        public void GetFollowersForUserTest()
        {
        }

        [Theory, AutoEF]
        public void IsUserFollowingTest(User currentUser, User targetUser, [Frozen] Mock<IDbSet<User>> userList,
            [Frozen] Mock<IUnitOfWork> unitOfWork, UserServices userServices)
        {
            //Setup
            userList.Setup(s => s.Find(It.Is((int i) => i == currentUser.Id))).Returns(currentUser);
            unitOfWork.Setup(s => s.GetDbSet<User>()).Returns(userList.Object);
            currentUser.Abonnements.Add(targetUser);

            //Execute
            var result = userServices.IsUserFollowing(currentUser.Id, targetUser.Id);

            //Verify
            Assert.True(result);
        }

        /// <summary>
        /// No need to test for the User not found, because the BaseServices Handles it for you
        /// </summary>
        [Theory, AutoEF]
        public void IsUserNotFollowingTest(User currentUser, User target, [Frozen] Mock<IDbSet<User>> userList,
            [Frozen] Mock<IUnitOfWork> unitOfWork, UserServices userServices)
        {
            //Setup
            userList.Setup(s => s.Find(It.Is((int i) => i == currentUser.Id))).Returns(currentUser);
            unitOfWork.Setup(s => s.GetDbSet<User>()).Returns(userList.Object);

            //Execute
            var result = userServices.IsUserFollowing(currentUser.Id, target.Id);

            //Verify
            Assert.False(result);
        }

        [Fact()]
        public void IsUserEmailUniqueTest()
        {
        }
    }
}