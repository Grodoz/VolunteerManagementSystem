using System.ComponentModel.DataAnnotations;

namespace VolunteerManagementSystem.Models
{
    public class VolunteerEvents
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        [StringLength(200)]
        public string EventName { get; set; } = string.Empty;
        [StringLength(1000)]
        public string? EventDescription { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }
        [StringLength(200)]
        public string? Location { get; set; }
        [Range(1, 10000)]
        public int Capacity { get; set; }
        public string? EventStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Registration> Registrations
        {
            get;
            set;
        } = new List<Registration>();
        public virtual ICollection<Attendance> AttendanceRecords
        {
            get; set;
        } = new List<Attendance>();
    }
}
