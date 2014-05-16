﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Streameus.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Streameus.DataAbstractionLayer.Contracts;

namespace Streameus.Controllers.Tests
{
    [TestClass()]
    public class RecommendationControllerTests
    {
        [TestMethod()]
        public void GetUserRecommendationsTest()
        {
            var userServicesMock = new Mock<IUserServices>();

            var controller = new RecommendationController(userServicesMock.Object);
            controller.GetUserRecommendations();
        }
    }
}