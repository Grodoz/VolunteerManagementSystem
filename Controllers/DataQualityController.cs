using Microsoft.AspNetCore.Mvc;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.ViewModels;

namespace VolunteerManagementSystem.Controllers
{
    public class DataQualityController : Controller
    {
        private readonly VolunteerManagementContext _context;

        public DataQualityController(VolunteerManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var volunteers =
        _context.Volunteers.ToList();

            var model =
                new DataQualityDashboardViewModel();

            model.TotalVolunteers =
                volunteers.Count;

            model.MissingEmails =
                volunteers.Count(v =>
                    string.IsNullOrWhiteSpace(v.EmailAddress));

            model.InvalidPhoneNumbers =
                volunteers.Count(v =>
                    string.IsNullOrWhiteSpace(v.PhoneNumber)
                    || v.PhoneNumber.Length < 10);

            model.DuplicateEmails =
                volunteers
                    .GroupBy(v => v.EmailAddress)
                    .Count(g =>
                        g.Count() > 1 &&
                        !string.IsNullOrWhiteSpace(g.Key));

            // Phase 5 starts here

            var totalIssues =
                model.DuplicateEmails +
                model.MissingEmails +
                model.InvalidPhoneNumbers;

            model.QualityScore =
                model.TotalVolunteers == 0
                    ? 100
                    : Math.Round(
                        100 -
                        ((double)totalIssues / model.TotalVolunteers * 100),
                        2);

            // Phase 5 ends here

            return View(model);
        }
    }
}
