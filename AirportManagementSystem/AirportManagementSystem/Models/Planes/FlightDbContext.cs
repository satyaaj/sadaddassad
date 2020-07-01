using AirportManagementSystem.Models.Pilot;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Planes
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext() : base("AirportManagementDb")
        {

        }
        public DbSet<FlightPlanDetails> Flights { get; set; }

        public System.Data.Entity.DbSet<PilotDetails> PilotDetails { get; set; }

        public System.Data.Entity.DbSet<PlaneDetails> PlaneDetails { get; set; }

        public System.Data.Entity.DbSet<UserRoles> UserRoles { get; set; }
    }
}