using Microsoft.AspNetCore.Mvc;
using VolunteerManagementSystem.Data;

namespace VolunteerManagementSystem.Controllers
{
    public class TestController : Controller
    {
        private readonly VolunteerManagementContext _context;
        public TestController(VolunteerManagementContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var count = _context.Volunteers.Count();
            return Content($"Volunteer Count: {count}");
        }
    }
}
