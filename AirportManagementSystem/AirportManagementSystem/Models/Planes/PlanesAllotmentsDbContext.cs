using AirportManagementSystem.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Planes
{
    public class PlanesAllotmentsDbContext : DbContext
    {
        public PlanesAllotmentsDbContext() : base("AirportManagementDb")
        {

        }
        public DbSet<PlanesAllotments> PlanesAllotments { get; set; }

        public System.Data.Entity.DbSet<HangarDetails> HangarDetails { get; set; }

        public System.Data.Entity.DbSet<PlaneDetails> PlaneDetails { get; set; }

        public System.Data.Entity.DbSet<ManagerDetails> ManagerDetails { get; set; }
    }
}