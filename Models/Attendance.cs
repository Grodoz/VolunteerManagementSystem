using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerManagementSystem.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        public int VolunteerId { get; set; }

        [Required]
        public int EventId { get; set; }

        public DateTime AttendanceDate { get; set; }

        [Range(0, 24)]
        public decimal HoursServed { get; set; }

        [StringLength(20)]
        public string AttendanceStatus { get; set; } = "Present";

        [ForeignKey("VolunteerId")]
        public virtual Volunteer? Volunteer { get; set; }

        [ForeignKey("EventId")]
        public virtual VolunteerEvents? Event { get; set; }
        
    }
}
