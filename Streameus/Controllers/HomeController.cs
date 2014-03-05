using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Streameus.DataAbstractionLayer;

namespace Streameus.Controllers
{
    public class HomeController : Controller
    {
        private StreameusContainer db = new StreameusContainer();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var users = db.Users.ToList();

            return View(users);
        }
    }
}