using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        /// Get about link to content in html format
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        [HttpGet]
        public string GetAbout()
        {
            //Retourne l'Url complete
            return Url.Link("Default", new {Controller = "Ressource", Action = "About"});
            //Retourne l'Url relative
//            return Url.Route("Default", new {Controller = "Ressource", Action = "About"});
        }

        // GET api/Resource/faq
        /// <summary>
        /// Get faq link to content in html format
        /// </summary>
        /// <returns></returns>
        [Route("faq")]
        [HttpGet]
        public string GetFaq()
        {
            return Url.Link("Default", new { Controller = "Ressource", Action = "Faq" });
        }

        // GET api/Resource/team
        /// <summary>
        /// Get team link to content in html format
        /// </summary>
        /// <returns></returns>
        [Route("team")]
        [HttpGet]
        public string GetTeam()
        {
            return Url.Link("Default", new { Controller = "Ressource", Action = "Team" });
        }
    }
}