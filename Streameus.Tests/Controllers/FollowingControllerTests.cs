using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Streameus.Controllers;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using NoResultException = Streameus.Exceptions.HttpErrors.NoResultException;


namespace Streameus.Tests.Controllers
{
    [TestClass()]
    public class FollowingControllerTests
    {
        [TestMethod]
        public void GetTest()
        {
            var userServicesMock = new Mock<IUserServices>();

            var returnedList = this.GetDummyUserList().Where(user => user.Id != 1);
            userServicesMock.Setup(s => s.GetAbonnementsForUser(1)).Returns(returnedList);

            var controller = new FollowingController(userServicesMock.Object);
            var followingList = controller.Get(1);
            Assert.AreEqual(7, followingList.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (NotFoundException))]
        public void GetTestUserNotFound()
        {
            var userServicesMock = new Mock<IUserServices>();

            userServicesMock.Setup(s => s.GetAbonnementsForUser(1)).Throws<Streameus.Exceptions.NoResultException>();

            var controller = new FollowingController(userServicesMock.Object);
            controller.Get(1);
        }

        [TestMethod]
        [ExpectedException(typeof (NoResultException))]
        public void GetTestUserEmptyResult()
        {
            var userServicesMock = new Mock<IUserServices>();

            userServicesMock.Setup(s => s.GetAbonnementsForUser(1)).Throws<Streameus.Exceptions.EmptyResultException>();

            var controller = new FollowingController(userServicesMock.Object);
            controller.Get(1);
        }


        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }


        private IQueryable<User> GetDummyUserList()
        {
            return new List<User>()
            {
                new User {Id = 1, Parameter = new Parameters(), FirstName = "Carson", LastName = "Alexander"},
                new User {Id = 2, Parameter = new Parameters(), FirstName = "Meredith", LastName = "Alonso"},
                new User {Id = 3, Parameter = new Parameters(), FirstName = "Arturo", LastName = "Anand", Gender = true},
                new User {Id = 4, Parameter = new Parameters(), FirstName = "Gytis", LastName = "Barzdukas"},
                new User {Id = 5, Parameter = new Parameters(), FirstName = "Yan", LastName = "Li"},
                new User
                {
                    Id = 6,
                    Parameter = new Parameters(),
                    FirstName = "Peggy",
                    LastName = "Justice",
                    Gender = false
                },
                new User
                {
                    Id = 7,
                    Parameter = new Parameters(),
                    FirstName = "Laura",
                    LastName = "Norman",
                    Gender = false
                },
                new User
                {
                    Id = 8,
                    Parameter = new Parameters(),
                    FirstName = "Nino",
                    LastName = "Olivetto",
                    Gender = true
                }
            }.AsQueryable();
        }
    }
}