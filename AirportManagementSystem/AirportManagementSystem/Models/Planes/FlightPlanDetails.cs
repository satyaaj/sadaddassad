using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Planes
{
    public class FlightPlanDetails
    {
        [Key]
        public int FlightPlanID { get; set; }
        [Required, Display(Name = "Plane ID")]

        [ForeignKey("PlaneID_IN_FD")]
        public string PlaneID { get; set; }
        public virtual PlaneDetails PlaneID_IN_FD { get; set; }
        [Display(Name = "Pilot ID")]
        public string PilotID { get; set; }

        [Required, Display(Name = "Departure Location")]
        public Locations DepartureLocation { get; set; }
        [Required, Display(Name = "Arrival Location")]
        public Locations ArrivalLocation { get; set; }

        [DataType(DataType.Time), Display(Name = "Departure Time")]
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Time), Display(Name = "Arrival Time")]
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ArrivalTime { get; set; }
        [Display(Name = "Is Active")]
        public bool isActive { get; set; }
    }

    public enum Locations
    {
        Hyderabad,
        Delhi,
        Chennai,
        Banglore,
        Califonia,
        London,
        Singapore,
        losangeles,
        NewYork,
        Thailand,
        Shanghai,
        Tokyo
    }
}