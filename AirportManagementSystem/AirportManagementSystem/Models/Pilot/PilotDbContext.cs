using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Pilot
{
    public class PilotDbContext: DbContext
    {
        public PilotDbContext() : base("AirportManagementDb")
        {

        }
        public DbSet<PilotDetails> Pilots { get; set; }
        public DbSet<PilotLogin> PLogins { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }
    }
}