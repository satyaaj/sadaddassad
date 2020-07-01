using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Manager
{
    public class ManagerLogin
    {
        public int ID { get; set; }
        [Required, Display(Name = "Username")]
        public string ManagerID { get; set; }
        [Required, Display(Name = "Password")]
        public string Password { get; set; }
    }
}