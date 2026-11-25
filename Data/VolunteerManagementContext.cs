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
        public DbSet<VolunteerEvents> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Attendance> Attendance { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Volunteer>()
                .HasIndex(v => v.EmailAddress)
                .IsUnique();
            modelBuilder.Entity<Registration>()
                .ToTable(t => t.HasCheckConstraint("CK_Registration_Status", "[RegistrationStatus] IN ('Registered', 'Waitlisted', 'Cancelled')"))
                .HasIndex(r => new
                {
                    r.VolunteerId,
                    r.EventId
                })
                .IsUnique();
            modelBuilder.Entity<VolunteerEvents>()
                .ToTable(t => t.HasCheckConstraint("CK_Events_Capacity", "[Capacity] > 0"));
        }
    }
}
