using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.Models;
using VolunteerManagementSystem.ViewModels;

namespace VolunteerManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly VolunteerManagementContext _context;
        public HomeController(VolunteerManagementContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var volunteers =
        _context.Volunteers.ToList();

            var model =
                new DashboardViewModel();

            model.TotalVolunteers =
                volunteers.Count;

            model.MissingEmails =
                volunteers.Count(v =>
                    string.IsNullOrWhiteSpace(
                        v.EmailAddress));

            model.InvalidPhoneNumbers =
                volunteers.Count(v =>
                    string.IsNullOrWhiteSpace(
                        v.PhoneNumber)
                    ||
                    v.PhoneNumber.Length < 10);

            model.DuplicateEmails =
                volunteers
                    .GroupBy(v => v.EmailAddress)
                    .Count(g =>
                        g.Count() > 1
                        &&
                        !string.IsNullOrWhiteSpace(g.Key));

            int totalIssues =
                model.DuplicateEmails +
                model.MissingEmails +
                model.InvalidPhoneNumbers;

            model.QualityScore =
                model.TotalVolunteers == 0
                    ? 100
                    : Math.Round(
                        100 -
                        ((double)totalIssues /
                         model.TotalVolunteers * 100),
                        2);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
