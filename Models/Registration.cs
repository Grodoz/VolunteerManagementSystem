using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerManagementSystem.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationId { get; set; }
        [Required]
        public int VolunteerId { get; set; }
        [Required]
        public int EventId { get; set; }
        public DateTime RegistrationDate { get; set; }
        [StringLength(25)]
        public string RegistrationStatus { get; set; } = "Registered";
        [ForeignKey("VolunteerId")]
        public virtual Volunteer? Volunteer { get; set; }
        [ForeignKey("EventId")]
        public virtual VolunteerEvents? Event { get; set; }

    }
}
