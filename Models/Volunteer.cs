using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerManagementSystem.Models
{
    public class Volunteer
    {
        public int VolunteerId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public virtual ICollection<Registration> Registrations
        {
            get;
            set;
        } = new List<Registration>();
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

    }
}
