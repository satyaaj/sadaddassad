using AirportManagementSystem.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AirportManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Root()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Root(SuperUser su)
        {
            if (ModelState.IsValid)
            {
                var context = new SuperUserContext();
                var retrive = context.Root.Where(n => n.UserName == su.UserName);
                foreach (var i in retrive)
                {
                    if (i.UserName == su.UserName && i.Password == su.Password)
                    {
                        FormsAuthentication.SetAuthCookie(su.UserName, false);
                        return RedirectToAction("Sudo", "Home");
                    }
                }
            }
            TempData["suError"] = "Invalid Username or Password";
            return View(su);
        }


        [Authorize(Roles = "sudo")]
        public ActionResult Sudo()
        {
            var adminUsers = new AdminDbContext();
            List<AdminDetails> retrive = adminUsers.Admin.ToList();
            return View(retrive);
        }

        [Authorize(Roles = "sudo")]
        public ActionResult AdminApproved(int id)
        {
            var context = new AdminDbContext();
            var user = context.Admin.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.ConfirmPassword = i.Password;
                i.isApproved = true;
                context.SaveChanges();
            }

            return RedirectToAction("Sudo");
        }

        [Authorize(Roles = "sudo")]
        public ActionResult AdminRejected(int id)
        {
            var context = new AdminDbContext();
            var user = context.Admin.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.ConfirmPassword = i.Password;
                i.isApproved = false;
                context.SaveChanges();
            }

            return RedirectToAction("Sudo");
        }
    }
}