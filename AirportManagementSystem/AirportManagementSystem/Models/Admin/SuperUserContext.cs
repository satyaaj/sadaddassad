using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Admin
{
    public class SuperUserContext: DbContext
    {
        public SuperUserContext() : base("AirportManagementDb")
        {

        }
        public DbSet<SuperUser> Root { get; set; }

        public System.Data.Entity.DbSet<AirportManagementSystem.Models.UserRoles> UserRoles { get; set; }
    }
}