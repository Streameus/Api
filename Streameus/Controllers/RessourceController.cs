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
            //Le viewbag est un tableau dynamique, tu set le title et on le recupere dans le layout pour mettre entre les balise title
            ViewBag.Title = "About";
            return View();
        }

        /// <summary>
        /// Get the Faq
        /// </summary>
        /// <returns>the Faq</returns>
        public ActionResult Faq()
        {
            ViewBag.Title = "FAQ";
            return View();
        }
    }
}