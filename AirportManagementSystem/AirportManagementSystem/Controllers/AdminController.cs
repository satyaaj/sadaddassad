using AirportManagementSystem.Models;
using AirportManagementSystem.Models.Admin;
using AirportManagementSystem.Models.Manager;
using AirportManagementSystem.Models.Pilot;
using AirportManagementSystem.Models.Planes;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace AirportManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult AdminLogin()
        {
            return View();
        }

        // Admin Login after post request
        [HttpPost, AllowAnonymous]
        public ActionResult AdminLogin(AdminLogin aLogin)
        {
            if (ModelState.IsValid)
            {
                var context = new AdminDbContext();
                var retrive = context.Admin.Where(n => n.AdminID == aLogin.AdminID);
                foreach (var i in retrive)
                {
                    if (i.isApproved == false)
                    {
                        TempData["superRoot"] = "SuperUser approval is needed";
                        return View(aLogin);
                    }
                    if (i.AdminID == (aLogin.AdminID) && i.Password == aLogin.Password && i.isApproved == true)
                    {
                        FormsAuthentication.SetAuthCookie(aLogin.AdminID, false);
                        return RedirectToAction("Index");
                    }
                }
            }
            TempData["adminLogin"] = "Invalid Username or password";
            return View(aLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult AdminRegister()
        {
            return View();
        }

        // Admin Register after post request
        [HttpPost, AllowAnonymous]
        public ActionResult AdminRegister(AdminDetails admin)
        {
            if (ModelState.IsValid)
            {
                var context = new AdminDbContext();
                var isUnique = context.Admin.Where(n => n.AdminID == admin.AdminID);
                foreach (var i in isUnique)
                {
                    if (i.AdminID == admin.AdminID)
                    {
                        ViewData["Error"] = "AdminID Already Exists";
                        return View(admin);
                    }
                }
                admin.RoleID = 1;
                context.Admin.Add(admin);
                context.SaveChanges();
                TempData["saved"] = "Admin Details Added to Database Sucessfull!";
                return RedirectToAction("AdminLogin");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Managers()
        {
            var context = new ManagerDbContext();
            List<ManagerDetails> manager = context.Managers.ToList();
            return View(manager);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ManagerApproved(int id)
        {
            var context = new ManagerDbContext();
            var user = context.Managers.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.ConfirmPassword = i.Password;
                i.isApproved = true;
                context.SaveChanges();
            }

            return RedirectToAction("Managers");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ManagerRejected(int id)
        {
            var context = new ManagerDbContext();
            var user = context.Managers.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.ConfirmPassword = i.Password;
                i.isApproved = false;
                context.SaveChanges();
            }

            return RedirectToAction("Managers");
        }

        [HttpGet, Authorize(Roles = "admin")]
        public ActionResult ManagerEdit(int? id)
        {
            var db = new ManagerDbContext();
            var model = db.Managers.FirstOrDefault(r => r.ID == id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "admin")]
        public ActionResult ManagerEdit(ManagerDetails manager)
        {
            var db = new ManagerDbContext();
            var user = db.Managers.Find(manager.ID);
            var entry = db.Entry(manager);
            if (ModelState.IsValid)
            {
                manager.RoleID = 2;

                entry.State = EntityState.Modified;
                db.Entry(user).Property("ConfirmPassword").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Managers", "Admin");
            }
            return View(entry);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Pilots()
        {
            var context = new PilotDbContext();
            List<PilotDetails> manager = context.Pilots.ToList();
            return View(manager);
        }

        [Authorize(Roles = "admin")]
        public ActionResult PilotApproved(int id)
        {
            var context = new PilotDbContext();
            var user = context.Pilots.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.ConfirmPassword = i.Password;
                i.isApproved = true;
                context.SaveChanges();
            }

            return RedirectToAction("Pilots");
        }

        [Authorize(Roles = "admin")]
        public ActionResult PilotRejected(int id)
        {
            var context = new PilotDbContext();
            var user = context.Pilots.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.ConfirmPassword = i.Password;
                i.isApproved = false;
                context.SaveChanges();
            }

            return RedirectToAction("Pilots");
        }

        [HttpGet, Authorize(Roles = "admin")]
        public ActionResult PilotEdit(int? id)
        {
            var db = new PilotDbContext();
            var model = db.Pilots.FirstOrDefault(r => r.ID == id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "admin")]
        public ActionResult PilotEdit(PilotDetails pilot)
        {
            var db = new PilotDbContext();
            var entry = db.Entry(pilot);
            if (ModelState.IsValid)
            {
                entry.State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Pilots", "Admin");
            }
            return View(entry);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddPlanes()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddPlanes(PlaneDetails planes)
        {
            if (ModelState.IsValid)
            {
                var context = new PlanesDbContext();
                var isUnique = context.Planes.Where(n => n.PlaneID == planes.PlaneID || n.OwnerID == planes.OwnerID);
                foreach (var i in isUnique)
                {
                    if (i.PlaneID == planes.PlaneID)
                    {
                        ViewData["PError"] = "PlaneID Already Exists";
                        return View(planes);
                    }
                    if(i.OwnerID == planes.OwnerID)
                    {
                        ViewData["OError"] = "OwnerID Already Exists";
                        return View(planes);
                    }
                }
                context.Planes.Add(planes);
                context.SaveChanges();
                TempData["planesSaved"] = "Planes Details Added Successfully!!";
                return RedirectToAction("Planes", "Admin");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Planes()
        {
            var context = new PlanesDbContext();
            List<PlaneDetails> planes = context.Planes.ToList();
            return View(planes);
        }

        [HttpGet, Authorize(Roles = "admin")]
        public ActionResult UpdatePlanes(string id)
        {
            var db = new PlanesDbContext();
            var model = db.Planes.FirstOrDefault(r => r.PlaneID == id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [HttpPost, Authorize(Roles = "admin")]
        public ActionResult UpdatePlanes(PlaneDetails plane)
        {
            var db = new PlanesDbContext();
            var entry = db.Entry(plane);
            if (ModelState.IsValid)
            {
                entry.State = EntityState.Modified;
                db.SaveChanges();
                TempData["planesUpdate"] = "Plane Details Updated Successfully";
                return RedirectToAction("Planes", "Admin");
            }
            return View(entry);
        }


        [HttpGet]
        public ActionResult AddHangar()
        {
            AdminDbContext db = new AdminDbContext();
            ViewBag.AdminID = new SelectList(db.Admin, "AdminID", "AdminID");
            return View();
        }

        [HttpPost]
        public ActionResult AddHangar(HangarDetails hangar)
        {
            if (ModelState.IsValid)
            {
                AllDbContext context = new AllDbContext();
                var isUnique = context.Hangars.Where(n => n.HangerName == hangar.HangerName);
                AdminDbContext db = new AdminDbContext();
                foreach (var i in isUnique)
                {
                    if (i.HangerName == hangar.HangerName)
                    {
                        ViewData["HError"] = "Hangar Name Already Exists";
                        return View(hangar);
                    }
                }
                    ViewBag.AdminID = new SelectList(db.Admin, "AdminID", "AdminID");
                var items = context.Admins.Select(n => n.AdminID);
                if (items != null)
                {
                    ViewBag.data = items;
                }

                hangar.AdminID = User.Identity.GetUserName();
                context.Hangars.Add(hangar);
                context.SaveChanges();
                TempData["hangarSaved"] = "Hangar Details Added to Database Successfully";
                return RedirectToAction("Hangars", "Admin");

            }
            return View();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Hangars()
        {
            var context = new HangarDbContext();
            List<HangarDetails> planes = context.Hangars.ToList();
            return View(planes);
        }

        [Authorize(Roles = "admin")]
        public ActionResult HangarApproved(int id)
        {
            var context = new HangarDbContext();
            var user = context.Hangars.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isActive = true;
                context.SaveChanges();
            }

            return RedirectToAction("Hangars");
        }

        [Authorize(Roles = "admin")]
        public ActionResult HangarRejected(int id)
        {
            var context = new HangarDbContext();
            var user = context.Hangars.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isActive = false;
                context.SaveChanges();
            }

            return RedirectToAction("Hangars");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateHangar(int? id)
        {
            var db = new AllDbContext();
            var model = db.Hangars.FirstOrDefault(r => r.ID == id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateHangar(HangarDetails hangar)
        {
            var db = new AllDbContext();
            var entry = db.Entry(hangar);
            if (ModelState.IsValid)
            {
                entry.State = EntityState.Modified;
                db.SaveChanges();
                TempData["HangarUpdate"] = "Hangar Details Updated Successfully";
                return RedirectToAction("Hangars", "Admin");
            }
            return View(entry);
        }



        [Authorize(Roles = "admin")]
        public ActionResult Reporting_Hangar()
        {
            var context = new AllDbContext();
            List<HangarDetails> hangar = context.Hangars.ToList();
            return View(hangar);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Reporting_Planes()
        {
            var context = new AllDbContext();
            List<PlaneDetails> planes = context.Planes.ToList();
            return View("Reporting_Planes", planes);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Reporting_Pilot()
        {
            var context = new AdminDbContext();
            List<PilotSchedule> pilotSchedule = context.PilotS.ToList();
            return View(pilotSchedule);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Reporting()
        {
            var context = new AllDbContext();
            List<PlaneDetails> planes = context.Planes.ToList();
            List<FlightPlanDetails> hangar = context.Flights.ToList();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult PilotSchedule()
        {
            var db = new AdminDbContext();
            List<PilotSchedule> pilotS = db.PilotS.ToList();
            return View(pilotS);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddSchedule(int? id)
        {
            var db = new AdminDbContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var db1 = new AllDbContext();
            PilotSchedule ps = db1.Pilot_schedule.Find(id);
            if (ps == null)
            {
                return HttpNotFound();
            }
            ViewBag.PilotID = new SelectList(db1.Pilots, "PilotID", "PilotID", ps.PilotID);
            return View(ps);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddSchedule(PilotSchedule ps)
        {
            var db = new AllDbContext();
            if (ModelState.IsValid)
            {
                db.Entry(ps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PilotID = new SelectList(db.Pilots, "PilotID", "PilotID", ps.PilotID);
            return View(ps);
        }


    }
}