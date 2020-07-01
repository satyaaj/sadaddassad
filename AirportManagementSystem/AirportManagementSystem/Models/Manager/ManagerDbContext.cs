using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Manager
{
    public class ManagerDbContext : DbContext
    {
        public ManagerDbContext() : base("AirportManagementDb")
        {

        }
        public DbSet<ManagerDetails> Managers { get; set; }
        public DbSet<ManagerLogin> MLogins { get; set; }

        public System.Data.Entity.DbSet<UserRoles> UserRoles { get; set; }
    }
}