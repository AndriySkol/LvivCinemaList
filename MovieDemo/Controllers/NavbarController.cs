using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDemo.Controllers
{
    public class NavbarController : Controller
    {
        //
        // GET: /Navbar/
        public ActionResult Navbar()
        {
            ViewBag.IsAuth = Request.IsAuthenticated;
            ViewBag.Name = User.Identity.Name;
            return PartialView();
        }
	}
}