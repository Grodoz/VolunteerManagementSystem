using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Data
{
    public class VolunteerManagementContext : DbContext
    {
        public VolunteerManagementContext(DbContextOptions<VolunteerManagementContext> options) : base(options)
        {

        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}
