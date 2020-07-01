using AirportManagementSystem.Models.Pilot;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagementSystem.Models.Admin
{
    public class PilotSchedule
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("PilotID_INPS"), Display(Name = "Pilot ID")]
        public string PilotID { get; set; }
        public virtual PilotDetails PilotID_INPS  { get; set; }
        [ForeignKey("AdminID_INPS"), Display(Name = "Admin ID")]
        public string AdminID { get; set; }
        public virtual AdminDetails AdminID_INPS { get; set; }
        [Display(Name = "Pilot Avaliabality From"), DisplayFormat(NullDisplayText = "N/A")]
        public DateTime? PilotAvailabilityFrom { get; set; }
        [Display(Name = "Pilot Avaliabality To"), DisplayFormat(NullDisplayText = "N/A")]
        public DateTime? PilotAvailabilityTo { get; set; }
        public bool IsActive { get; set; }
    }
}