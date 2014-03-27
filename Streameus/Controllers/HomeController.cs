using Streameus.Models;
#pragma warning disable 1591
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
        private StreameusContext db = new StreameusContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return this.Redirect("/swagger");
        }
    }
}

#pragma warning restore 1591