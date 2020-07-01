using AirportManagementSystem.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Planes
{
    public class HangarDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Key, Required]
        [Display(Name = "Hangar Name")]
        public string HangerName { get; set; }
        [ForeignKey("AdminID_IN_FD")]
        [Display(Name = "Admin ID")]
        public string AdminID { get; set; }
        public AdminDetails AdminID_IN_FD { get; set; }
        [Display(Name = "Terminal")]
        public PlaneType Terminal { get; set; }
        [Display(Name = "Is Active")]
        public bool isActive { get; set; }
        [Display(Name = "Is Plane Allocated")]
        public bool isPlaneAllocated { get; set; }
        public virtual ICollection<PlanesAllotments> Hangar_IN_PA { get; set; }
    }
}