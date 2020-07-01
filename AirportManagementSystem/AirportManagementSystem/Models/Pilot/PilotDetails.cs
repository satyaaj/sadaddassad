using AirportManagementSystem.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AirportManagementSystem.Models.Pilot
{
    public class PilotDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, MaxLength(225), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(225), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, Display(Name = "Age")]
        public short Age { get; set; }

        [Display(Name = "Gender")]
        public GenderType Gender { get; set; }

        [Required, Display(Name = "Contact Number"), RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string ContactNumber { get; set; }

        [Key, Required, Display(Name = "Username")]
        public string PilotID { get; set; }

        [Required, MaxLength(225), Display(Name = "Password")]
        public string Password { get; set; }
        [Required, NotMapped, Compare("Password"), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Is Approved")]
        public bool isApproved { get; set; }
        [ForeignKey("PilotRole")]
        public int? RoleID { get; set; }
        public virtual UserRoles PilotRole { get; set; }
        public virtual ICollection<PilotSchedule> PilotID_INPS { get; set; }
    }
}