using AirportManagementSystem.Models.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Planes
{
    public class PlanesAllotments
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("Hangar_IN_PA")]
        [Display(Name = "Hangar Name")]
        public string HangarName { get; set; }
        public HangarDetails Hangar_IN_PA { get; set; }

        [Required]
        [ForeignKey("Plane_IN_PA")]
        [Display(Name = "Plane ID")]
        public string PlaneID { get; set; }
        public PlaneDetails Plane_IN_PA { get; set; }
        [ForeignKey("ManagerID_IN_PA")]
        [Display(Name = "Manager ID")]
        public string ManagerID { get; set; }
        public ManagerDetails ManagerID_IN_PA { get; set; }
        [Required, Display(Name = "Is Plane Allocated")]
        public bool isPlaneAllocated { get; set; }
    }
}