using AirportManagementSystem.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Planes
{
    public class HangarDbContext: DbContext
    {
        public HangarDbContext() : base("AirportManagementDb")
        {

        }
        public DbSet<HangarDetails> Hangars { get; set; }

        public System.Data.Entity.DbSet<AdminDetails> AdminDetails { get; set; }
    }
}