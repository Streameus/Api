using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Streameus.Controllers
{
    /// <summary>
    /// Controller retournant toutes les pages de ressource.
    /// Ces pages utilisent la vue _LayoutApi.cshtml comme layout de base
    /// </summary>
    public class RessourceController : Controller
    {
        //
        // GET: /Ressource/
        /// <summary>
        /// Get the About through the api
        /// </summary>
        /// <returns>The about page</returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Get the Faq through the api
        /// </summary>
        /// <returns>the Faq</returns>
        public ActionResult Faq()
        {
            return View();
        }

        /// <summary>
        /// Get the team through the api
        /// </summary>
        /// <returns>the Faq</returns>
        public ActionResult Team()
        {
            return View();
        }
    }
}