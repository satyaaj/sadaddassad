using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Admin
{
    public class AdminDbContext: DbContext
    {
        public AdminDbContext() : base("AirportManagementDb")
        {
        }
        public DbSet<AdminDetails> Admin { get; set; }

        public DbSet<AdminLogin> AdminLogins { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<PilotSchedule> PilotS { get; set; }

        public System.Data.Entity.DbSet<AirportManagementSystem.Models.Pilot.PilotDetails> PilotDetails { get; set; }

        public System.Data.Entity.DbSet<AirportManagementSystem.Models.Planes.PlaneDetails> PlaneDetails { get; set; }
    }
}