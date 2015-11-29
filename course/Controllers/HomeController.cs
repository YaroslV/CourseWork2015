using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using course.Models;
using AspNet.Identity.CustomDatabase;

namespace course.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new ApplicationDbContext()));
            if(userId == null)
            {
                return View();
            }
            else if(userManager.IsInRole(userId, Role.Student.ToString()))
            {
                return RedirectToAction("StudentIndex","Home");
            }
            else if(userManager.IsInRole(userId, Role.Tutor.ToString()))
            {
                return RedirectToAction("TutorIndex","Home");
            }
            else if(userManager.IsInRole(userId, "Admin"))
            {
                return RedirectToAction("AdminIndex","Home");
            }

            return View();            
        }

        [Authorize(Roles ="Student")]
        public ActionResult StudentIndex()
        {
            return View();
        }
        [Authorize(Roles ="Tutor")]
        public ActionResult TutorIndex()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}