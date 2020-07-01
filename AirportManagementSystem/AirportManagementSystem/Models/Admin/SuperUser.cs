using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Admin
{
    public class SuperUser
    {
        [Key, Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [ForeignKey("Su")]
        public int? RoleID { get; set; }
        public virtual UserRoles Su { get; set; }
    }
}