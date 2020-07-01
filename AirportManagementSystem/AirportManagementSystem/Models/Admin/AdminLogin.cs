using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Admin
{
    public class AdminLogin
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Username")]
        public string AdminID { get; set; }
        [Required, Display(Name = "Password")]
        public string Password { get; set; }
    }
}