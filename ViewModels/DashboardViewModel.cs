namespace VolunteerManagementSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalVolunteers { get; set; }
        public int DuplicateEmails { get; set; }
        public int MissingEmails { get; set; }
        public int InvalidPhoneNumbers { get; set; }
        public double QualityScore { get; set; }
        public int OpenDataQualityIssues
        {
            get
            {
                return DuplicateEmails + MissingEmails + InvalidPhoneNumbers;
            }
        }
    }
}
