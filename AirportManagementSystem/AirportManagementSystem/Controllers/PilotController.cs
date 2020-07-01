using AirportManagementSystem.Models;
using AirportManagementSystem.Models.Admin;
using AirportManagementSystem.Models.Pilot;
using AirportManagementSystem.Models.Planes;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AirportManagementSystem.Controllers
{
    public class PilotController : Controller
    {
        // GET: Pilot
        [Authorize(Roles = "pilot")]
        public ActionResult Index()
        {
            var context = new FlightDbContext();
            var UserID = User.Identity.GetUserName();
            List<FlightPlanDetails> planes = context.Flights.Where(n => n.PilotID == UserID).ToList();
            return View(planes);
        }


        [HttpGet]
        public ActionResult PilotRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PilotRegister(PilotDetails admin)
        {
            var pilot = new PilotDetails
            {
                PilotID = admin.PilotID,
                Password = admin.Password,
                ConfirmPassword = admin.ConfirmPassword,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Age = admin.Age,
                Gender = admin.Gender,
                ContactNumber = admin.ContactNumber,
                RoleID = admin.RoleID,
                isApproved = admin.isApproved
            };
            if (ModelState.IsValid)
            {
                var context = new PilotDbContext();
                var isUnique = context.Pilots.Where(n => n.PilotID == admin.PilotID);
                foreach (var i in isUnique)
                {
                    if (i.PilotID == admin.PilotID)
                    {
                        ViewData["Error"] = "PilotID Already Exists";
                        return View(admin);
                    }
                }

                admin.RoleID = 3;
                context.Pilots.Add(admin);
                context.SaveChanges();

                var pilot_schedule = new PilotSchedule()
                {
                    PilotID = admin.PilotID,
                    PilotAvailabilityFrom = null,
                    PilotAvailabilityTo = null,
                    IsActive = false
                };

                var pils = new AdminDbContext();
                pils.PilotS.Add(pilot_schedule);
                pils.SaveChanges();

                TempData["saved"] = "Pilot Details Added Successfully!";
                return RedirectToAction("PilotLogin");
            }
            return View();
        }

        [HttpGet]
        public ActionResult PilotLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PilotLogin(PilotLogin pLogin)
        {
            if (ModelState.IsValid)
            {
                var context = new PilotDbContext();
                var retrive = context.Pilots.Where(n => n.PilotID == pLogin.PilotID);
                foreach (var i in retrive)
                {
                    if (i.isApproved == false)
                    {
                        TempData["Message"] = "Admin approval is needed";
                        return View(pLogin);
                    }
                    if (i.PilotID == pLogin.PilotID && i.Password == pLogin.Password && i.isApproved == true)
                    {
                        FormsAuthentication.SetAuthCookie(pLogin.PilotID.ToString(), false);
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewData["Error"] = "Invalid Username or password";
            return View(pLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }


        [HttpGet]
        [Authorize(Roles = "pilot")]
        public ActionResult FlightDetailsUpdate()
        {
            PlanesDbContext db = new PlanesDbContext();
            var ps = new AdminDbContext();
            var user = User.Identity.GetUserName();
            var ps1 = ps.PilotS.FirstOrDefault(n => n.PilotID == user);
            var from = ps1.PilotAvailabilityFrom;
            ViewBag.PlaneID = new SelectList(db.Planes.Where(n => n.isAllotted == false), "PlaneID", "PlaneID");
            TempData["planSchedule"] = $"You can create Flight Plan in between {ps1.PilotAvailabilityFrom} to {ps1.PilotAvailabilityTo}";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "pilot")]
        public ActionResult FlightDetailsUpdate(FlightPlanDetails flight)
        {
            if (ModelState.IsValid)
            {
                AllDbContext context = new AllDbContext();
                PlanesDbContext db = new PlanesDbContext();

                ViewBag.PlaneID = new SelectList(db.Planes.Where(n => n.isAllotted == false), "PlaneID", "PlaneID");


                var items = context.Planes.Where(n => n.isAllotted == false);
                ViewBag.PlaneId = items;
                if (items != null)
                {
                    ViewBag.data = items;
                }
                var pilot = db.Planes.Where(n => n.PlaneID == flight.PlaneID);
                foreach (var i in pilot)
                {
                    i.isAllotted = true;
                }
                flight.isActive = true;
                flight.PilotID = User.Identity.GetUserName();
                context.Flights.Add(flight);
                db.SaveChanges();
                context.SaveChanges();
                TempData["savedFlight"] = "Flight Details Added to Database Sucessfull!";
                return RedirectToAction("Index", "Pilot");
            }
            return View();
        }

        [Authorize(Roles = "pilot")]
        public ActionResult ViewSchedule(int? id)
        {

            var context = new AdminDbContext();
            var UserID = User.Identity.GetUserName();
            List<PilotSchedule> planes = context.PilotS.Where(n => n.PilotID == UserID).ToList();
            return View(planes);
        }


        [HttpGet]
        [Authorize(Roles = "pilot")]
        public ActionResult UpdateFlightPlan(string id)
        {
            var db = new PlanesDbContext();
            var db1 = new FlightDbContext();
            ViewBag.PlaneID = new SelectList(db.Planes.Where(n => n.isAllotted == false), "PlaneID", "PlaneID");
            var flight = db1.Flights.FirstOrDefault(n => n.PlaneID == id);
            return View(flight);
        }

        [HttpPost]
        [Authorize(Roles = "pilot")]
        public ActionResult UpdateFlightPlan(FlightPlanDetails flightPlan)
        {
            PlanesDbContext db = new PlanesDbContext();
            var db1 = new FlightDbContext();
            ViewBag.PlaneID = new SelectList(db.Planes.Where(n => n.isAllotted == false), "PlaneID", "PlaneID");
            var flight = db1.Flights.FirstOrDefault(n => n.PlaneID == flightPlan.PlaneID);

            db1.SaveChanges();


            return View();
        }

        [Authorize(Roles = "pilot")]
        public ActionResult pilotActive(int? id)
        {
            var context = new AdminDbContext();
            var user = context.PilotS.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.IsActive = true;
                context.SaveChanges();
            }
            return RedirectToAction("ViewSchedule");
        }

        [Authorize(Roles = "pilot")]
        public ActionResult pilotOnLeave(int? id)
        {
            var context = new AdminDbContext();
            var user = context.PilotS.Where(n => n.ID == id).ToList();
            foreach(var i in user)
            {
                i.IsActive = false;
                context.SaveChanges();
            }
            return RedirectToAction("ViewSchedule");
        }


    }
}