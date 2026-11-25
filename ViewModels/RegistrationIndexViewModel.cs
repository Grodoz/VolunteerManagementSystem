using Microsoft.Identity.Client;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.ViewModels
{
    public class RegistrationIndexViewModel
    {
        public List<Registration> Registrations { get; set; } = new();
        public int? SelectedEventId { get; set; }
        public string? SelectedStatus { get; set; }
        public int TotalRegistrations { get; set; }
        public int RegisteredCount { get; set; }
        public int WaitlistedCount { get; set; }
        public int CancelledCount { get; set; }
        public int NearCapacityEvents { get; set; }
        public List<string> CapacityAlerts { get; set; } = new();
        public int TotalAttendanceRecords { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int ExcusedCount { get; set; }
        public decimal TotalVolunteerHours { get; set; }
        public int AttendanceExceptions { get; set; }
        public int PresentZeroHoursCount { get; set; }
        public int ExcessiveHoursCount { get; set; }
        public int AttendanceWithoutRegistrationCount { get; set; }
        public int MissingAttendanceStatusCount { get; set; }
    }
}
