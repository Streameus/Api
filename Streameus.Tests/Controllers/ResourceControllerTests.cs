using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Streameus.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Streameus.Controllers.Tests
{
    [TestClass()]
    public class ResourceControllerTests
    {
        [TestMethod()]
        public void GetAboutTest()
        {
            ResourceController resource = new ResourceController();
            string ret = resource.GetAbout();
            Assert.AreEqual("http://localhost:1281/api/Resource/about", ret);
            Assert.Inconclusive("Ce test fonctionne qu'en localhost pour le moment");
        }

        [TestMethod()]
        public void GetFaqTest()
        {
            ResourceController resource = new ResourceController();
            string ret = resource.GetFaq();
            Assert.AreEqual("http://localhost:1281/api/Resource/faq", ret);
            Assert.Inconclusive("Ce test fonctionne qu'en localhost pour le moment");
        }

        [TestMethod()]
        public void GetTeamTest()
        {
            ResourceController resource = new ResourceController();
            string ret = resource.GetTeam();
            Assert.AreEqual("http://localhost:1281/api/Resource/team", ret);
            Assert.Inconclusive("Ce test fonctionne qu'en localhost pour le moment");
        }
    }
}
