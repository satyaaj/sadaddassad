using AirportManagementSystem.Models;
using AirportManagementSystem.Models.Manager;
using AirportManagementSystem.Models.Planes;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AirportManagementSystem.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        [Authorize(Roles = "manager")]
        public ActionResult Index()
        {
            var context = new PlanesAllotmentsDbContext();
            List<PlanesAllotments> planes = context.PlanesAllotments.ToList();
            return View(planes);
        }


        [HttpGet]
        public ActionResult ManagerRegister()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult ManagerRegister(ManagerDetails admin)
        {
            if (ModelState.IsValid)
            {
                var context = new ManagerDbContext();
                var isUnique = context.Managers.Where(n => n.ManagerID == admin.ManagerID);
                foreach (var i in isUnique)
                {
                    if (i.ManagerID == admin.ManagerID)
                    {
                        ViewData["Error"] = "ManagerID Already Exists";
                        return View(admin);
                    }
                }
                admin.RoleID = 2;
                context.Managers.Add(admin);
                context.SaveChanges();

                TempData["Msaved"] = "Manager Registration Successfully!";
                return RedirectToAction("ManagerLogin");
            }
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult ManagerLogin()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult ManagerLogin(ManagerLogin mLogin)
        {
            if (ModelState.IsValid)
            {
                var context = new ManagerDbContext();
                var retrive = context.Managers.Where(n => n.ManagerID == mLogin.ManagerID);
                foreach (var i in retrive)
                {
                    if (i.isApproved == false)
                    {
                        TempData["Message"] = "Admin approval is needed";
                        return View(mLogin);
                    }
                    if (i.ManagerID == mLogin.ManagerID && i.Password == mLogin.Password && i.isApproved == true)
                    {
                        FormsAuthentication.SetAuthCookie(mLogin.ManagerID, false);
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewData["Error"] = "Invalid Username or password";
            return View(mLogin);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }



        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult AllotmentOfPlanes()
        {
            AllDbContext db = new AllDbContext();
            ViewBag.HangarName = new SelectList(db.Hangars.Where(n => n.isPlaneAllocated == false && n.isActive == true), "HangerName", "HangerName");
            ViewBag.PlaneID = new SelectList(db.Planes.Where(n => n.isAllotted == false), "PlaneID", "planeID");
            ViewBag.ManagerID = User.Identity.GetUserName();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult AllotmentOfPlanes(PlanesAllotments hangar)
        {
            AllDbContext db = new AllDbContext();
            var user = db.Hangars.Where(n => n.HangerName == hangar.HangarName);
            var planeUser = db.Planes.Where(n => n.PlaneID == hangar.PlaneID);

            if (ModelState.IsValid)
            {
                foreach (var i in user)
                {
                    i.isPlaneAllocated = true;
                }
                foreach (var j in planeUser)
                {
                    j.isAllotted = true;
                }
                hangar.isPlaneAllocated = true;
                hangar.ManagerID = User.Identity.GetUserName().ToString();
                db.Entry(hangar).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HangarName = new SelectList(db.Hangars.Where(n => n.isPlaneAllocated == false && n.isActive == true), "HangerName", "HangerName");
            ViewBag.PlaneID = new SelectList(db.Planes.Where(n => n.isAllotted == false), "PlaneID", "planeID");
            return View(hangar);
        }

        [Authorize(Roles = "manager")]
        public ActionResult PlaneApproved(int id)
        {
            var context = new PlanesAllotmentsDbContext();
            var hangar = new HangarDbContext();
            var user1 = hangar.Hangars.Where(n => n.ID == id);
            var user = context.PlanesAllotments.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isPlaneAllocated = true;
                context.SaveChanges();
            }
            foreach (var i in user1)
            {
                i.isPlaneAllocated = true;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "manager")]
        public ActionResult PlaneRejected(int id)
        {
            var context = new PlanesAllotmentsDbContext();
            var user = context.PlanesAllotments.Where(n => n.ID == id).ToList();
            var hangar = new HangarDbContext();
            var user1 = hangar.Hangars.Where(n => n.ID == id);
            foreach (var i in user)
            {
                i.isPlaneAllocated = false;
                context.SaveChanges();
            }
            foreach (var i in user1)
            {
                i.isPlaneAllocated = false;
                i.isActive = false;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}