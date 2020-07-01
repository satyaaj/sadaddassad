using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Planes
{
    public class PlanesDbContext: DbContext
    {
        public PlanesDbContext() : base("AirportManagementDb")
        {

        }

        public DbSet<PlaneDetails> Planes { get; set; }
    }
}