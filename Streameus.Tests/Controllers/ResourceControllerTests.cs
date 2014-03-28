using Microsoft.VisualStudio.TestTools.UnitTesting;
using Streameus.Controllers;

namespace Streameus.Tests.Controllers
{
    [TestClass()]
    public class ResourceControllerTests
    {
        [TestMethod()]
        public void GetAboutTest()
        {
            Assert.Inconclusive("Test non fonctionnelle a cause du Url.link dans resourceController");
            var resource = new ResourceController();
            string ret = resource.GetAbout();           
            Assert.AreEqual("http://localhost:1281/api/Resource/about", ret);
        }

        [TestMethod()]
        public void GetFaqTest()
        {
            Assert.Inconclusive("Test non fonctionnelle a cause du Url.link dans resourceController");
            var resource = new ResourceController();
            string ret = resource.GetFaq();
            Assert.AreEqual("http://localhost:1281/api/Resource/faq", ret);
        }

        [TestMethod()]
        public void GetTeamTest()
        {
            Assert.Inconclusive("Test non fonctionnelle a cause du Url.link dans resourceController");
            var resource = new ResourceController();
            string ret = resource.GetTeam();
            Assert.AreEqual("http://localhost:1281/api/Resource/team", ret);
        }
    }
}
