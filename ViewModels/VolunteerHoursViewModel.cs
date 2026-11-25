namespace VolunteerManagementSystem.ViewModels
{
    public class VolunteerHoursViewModel
    {
        public int VolunteerId { get; set; }
        public string VolunteerName { get; set; } = string.Empty;
        public decimal TotalHours { get; set; }
        public int EventsAttended { get; set; }
    }
}
