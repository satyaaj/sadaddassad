using AirportManagementSystem.Models.Admin;
using AirportManagementSystem.Models.Manager;
using AirportManagementSystem.Models.Pilot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models
{
    public class UserRoles
    {
        [Key]
        public int ID { get; set; }
        public string RolesName { get; set; }
        public int? RoleID { get; set; }

        public virtual ICollection<AdminDetails> AdminRole { get; set; }
        public virtual ICollection<ManagerDetails> ManagerRole { get; set; }
        public virtual ICollection<PilotDetails> PilotRole { get; set; }
        public virtual ICollection<SuperUser> Su { get; set; }
    }
}