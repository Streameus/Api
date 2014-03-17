using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Streameus.Controllers
{
    /// <summary>
    /// Resource controller
    /// </summary>
    [System.Web.Http.RoutePrefix("api/Resource")]
    public class ResourceController : ApiController
    {
        // GET api/Resource/about
        /// <summary>
        /// Get about content in html format
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Route("about")]
        [System.Web.Http.HttpGet]
        public string GetAbout()
        {
            //Retourne l'Url complete
            return Url.Link("Default", new {Controller = "Ressource", Action = "About"});
            //Retourne l'Url relative
            return Url.Route("Default", new {Controller = "Ressource", Action = "About"});
            //            return "about"; //todo read about.html from Resources and return it.
        }

        // GET api/Resource/faq
        /// <summary>
        /// Get faq content in html format
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Route("faq")]
        [System.Web.Http.HttpGet]
        public string GetFaq()
        {
            return "faq"; //todo read faq.html from Resources and return it. Or maybe return a list in json.
        }

        // GET api/Resource/team
        /// <summary>
        /// Get team content in html format
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Route("team")]
        [System.Web.Http.HttpGet]
        public string GetTeam()
        {
            return "team"; //todo read team.html from Resources and return it. Or maybe return a list in json.
        }
    }
}