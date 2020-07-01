using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirportManagementSystem.Models.Planes
{
    public class PlaneDetails
    {
        [Key]
        [Required]
        public string PlaneID { get; set; }

        [Required, Display(Name = "OwnerID")]
        public string OwnerID { get; set; }

        [Required, Display(Name = "Owner First Name")]
        public string OwnerFirstName { get; set; }

        [Required, Display(Name = "Owner Last Name")]
        public string OwnerLastName { get; set; }

        [Required, Display(Name = "Owner Email"), DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; }

        [Required, Display(Name = "Plane Type")]
        public PlaneType PlaneType { get; set; }

        [Required, Display(Name = "Plane Capacity")]
        public int PlaneCapacity { get; set; }
        [Display(Name = "Is Allotted")]
        public bool isAllotted { get; set; }

        public virtual ICollection<FlightPlanDetails> PlaneID_IN_FD { get; set; }
        public virtual ICollection<PlanesAllotments> Plane_IN_PA { get; set; }
    }

    public enum PlaneType
    {
        International,
        Domestic
    }
}