using AirportManagementSystem.Models.Admin;
using AirportManagementSystem.Models.Manager;
using AirportManagementSystem.Models.Pilot;
using AirportManagementSystem.Models.Planes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models
{
    public class AllDbContext: DbContext
    {
        public AllDbContext() : base("AirportManagementDb")
        {

        }
        public DbSet<AdminDetails> Admins { get; set; }
        public DbSet<ManagerDetails> Managers { get; set; }
        public DbSet<PilotDetails> Pilots { get; set; }
        public DbSet<UserRoles> userroles { get; set; }
        public DbSet<PlaneDetails> Planes { get; set; }
        public DbSet<PilotSchedule> Pilot_schedule { get; set; }

        public DbSet<FlightPlanDetails> Flights { get; set; }
        public DbSet<HangarDetails> Hangars { get; set; }
        public DbSet<PlanesAllotments> PlanesAllotments { get; set; }
        public DbSet<SuperUser> SuperUser { get; set; }
    }
}