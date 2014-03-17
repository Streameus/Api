using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Streameus.Controllers
{
    /// <summary>
    /// Resource controller
    /// </summary>
    [RoutePrefix("api/Resource")]
    public class ResourceController : ApiController
    {
        // GET api/Resource/about
        /// <summary>
        /// Get about content in html format
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        [HttpGet]
        public string GetAbout()
        {
            return "about"; //todo read about.html from Resources and return it.
        }

        // GET api/Resource/faq
        /// <summary>
        /// Get faq content in html format
        /// </summary>
        /// <returns></returns>
        [Route("faq")]
        [HttpGet]
        public string GetFaq()
        {
            return "faq";//todo read faq.html from Resources and return it. Or maybe return a list in json.
        }

        // GET api/Resource/team
        /// <summary>
        /// Get team content in html format
        /// </summary>
        /// <returns></returns>
        [Route("team")]
        [HttpGet]
        public string GetTeam()
        {
            return "team";//todo read team.html from Resources and return it. Or maybe return a list in json.
        }

    }
}