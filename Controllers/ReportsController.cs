using Microsoft.AspNetCore.Mvc;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace VolunteerManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly VolunteerManagementContext _context;
        public ReportsController(VolunteerManagementContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> VolunteerHours()
        {
            var leaderboard = await _context.Attendance
                .Include(a => a.Volunteer)
                .GroupBy(a => new
                {
                    a.VolunteerId,
                    a.Volunteer!.FirstName,
                    a.Volunteer.LastName
                })
                .Select(g => new VolunteerHoursViewModel
                {
                    VolunteerId = g.Key.VolunteerId,
                    VolunteerName = g.Key.FirstName + " " + g.Key.LastName,
                    TotalHours = g.Sum(x => x.HoursServed),
                    EventsAttended = g.Count()
                })
                .OrderByDescending(v => v.TotalHours)
                .ToListAsync();
            return View(leaderboard);
        }
    }
}
