using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Models;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.ViewModels;

public class RegistrationsController : Controller
{
    private readonly VolunteerManagementContext _context;

    public RegistrationsController(VolunteerManagementContext context)
    {
        _context = context;
    }

    // GET: REGISTRATIONS
    public async Task<IActionResult> Index(int? eventId, string? status)    
    {
        var query = _context.Registrations
            .Include(r => r.Volunteer)
            .Include(r => r.Event)
            .AsQueryable();
        var attendanceRecords = await _context.Attendance.ToListAsync();
        var presentZeroHours = attendanceRecords.Count(a => a.AttendanceStatus == "Present" && a.HoursServed == 0);
        var excessiveHours = attendanceRecords.Count(a => a.HoursServed >= 12);
        var missingStatus = attendanceRecords.Count(a => string.IsNullOrWhiteSpace(a.AttendanceStatus));
        var attendanceWithoutRegistration = attendanceRecords.Count(a => !_context.Registrations.Any(r => r.VolunteerId == a.VolunteerId && r.EventId == a.EventId));
        if (eventId.HasValue)
        {
            query = query.Where(r => r.EventId == eventId.Value);
        }
        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(r => r.RegistrationStatus == status);
        }
        ViewBag.Events = new SelectList(_context.Events.OrderBy(e => e.EventName), "EventId", "EventName");
        ViewBag.Statuses = new SelectList(new[] { "Registered", "Waitlisted", "Cancelled" });
        var allRegistrations = await _context.Registrations.ToListAsync();
        var capacityAlerts = _context.Events.Select(e => new
        {
            Event = e,
            RegistrationCount = _context.Registrations.Count(r => r.EventId == e.EventId)
        }).Where(x => x.RegistrationCount >= (x.Event.Capacity * 0.80)).ToList();
        var model = new RegistrationIndexViewModel
        {
            Registrations = await query.ToListAsync(),
            SelectedEventId = eventId,
            SelectedStatus = status,
            TotalRegistrations = allRegistrations.Count,
            RegisteredCount = allRegistrations.Count(r => r.RegistrationStatus == "Registered"),
            WaitlistedCount = allRegistrations.Count(r => r.RegistrationStatus == "Waitlisted"),
            CancelledCount = allRegistrations.Count(r => r.RegistrationStatus == "Cancelled"),
            NearCapacityEvents = capacityAlerts.Count,
            CapacityAlerts = capacityAlerts.Select(x => $"{x.Event.EventName} ({x.RegistrationCount}/{x.Event.Capacity})").ToList(),
            TotalAttendanceRecords = attendanceRecords.Count,
            PresentCount = attendanceRecords.Count(a => a.AttendanceStatus == "Present"),
            AbsentCount = attendanceRecords.Count(a => a.AttendanceStatus == "Absent"),
            ExcusedCount = attendanceRecords.Count(a => a.AttendanceStatus == "Excused"),
            TotalVolunteerHours = attendanceRecords.Sum(a => a.HoursServed),
            PresentZeroHoursCount = presentZeroHours,
            ExcessiveHoursCount = excessiveHours,
            AttendanceWithoutRegistrationCount = attendanceWithoutRegistration,
            MissingAttendanceStatusCount = missingStatus,
            AttendanceExceptions = presentZeroHours + excessiveHours + attendanceWithoutRegistration + missingStatus,

        };
        return View(model);
    }

    // GET: REGISTRATIONS/Details/5
    public async Task<IActionResult> Details(int? registrationid)
    {
        if (registrationid == null)
        {
            return NotFound();
        }

        var registration = await _context.Registrations
            .Include(r => r.Volunteer)
            .Include(r => r.Event)
            .FirstOrDefaultAsync(r => r.RegistrationId == registrationid);
        if (registration == null)
        {
            return NotFound();
        }

        return View(registration);
    }

    // GET: REGISTRATIONS/Create
    public IActionResult Create()
    {
        LoadDropDowns();
        return View();
    }

    // POST: REGISTRATIONS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RegistrationId,VolunteerId,EventId,RegistrationDate,RegistrationStatus,Volunteer,Event")] Registration registration)
    {
        var duplicateRegistration = _context.Registrations.Any(r => r.VolunteerId == registration.VolunteerId && r.EventId == registration.EventId);
        if (duplicateRegistration)
        {
            ModelState.AddModelError("", "Volunteer is already registered for this event.");
        }
        var selectedEvent = await _context.Events
            .FirstOrDefaultAsync(e => e.EventId == registration.EventId);
        if (selectedEvent != null)
        {
            var eventRegistrationCount = _context.Registrations.Count(r => r.EventId == registration.EventId);
            if (eventRegistrationCount >= selectedEvent.Capacity)
            {
                ModelState.AddModelError("", "The event has reached its maximum capacity.");
            }
        }
        if (ModelState.IsValid)
        {
            registration.RegistrationDate = DateTime.Now;
            _context.Add(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        LoadDropDowns();
        return View(registration);
    }

    // GET: REGISTRATIONS/Edit/5
    public async Task<IActionResult> Edit(int? registrationid)
    {
        if (registrationid == null)
        {
            return NotFound();
        }

        var registration = await _context.Registrations.FindAsync(registrationid);
        if (registration == null)
        {
            return NotFound();
        }
        return View(registration);
    }

    // POST: REGISTRATIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? registrationid, [Bind("RegistrationId,VolunteerId,EventId,RegistrationDate,RegistrationStatus,Volunteer,Event")] Registration registration)
    {
        if (registrationid != registration.RegistrationId)
        {
            return NotFound();
        }

        var duplicateRegistration = _context.Registrations.Any(r => r.RegistrationId != registration.RegistrationId && r.VolunteerId == registration.VolunteerId && r.EventId == registration.EventId);
        if (duplicateRegistration)
        {
            ModelState.AddModelError("", "Volunteer is already registered for this event.");
        }
        var selectedEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventId == registration.EventId);
        if (selectedEvent != null)
        {
            var eventRegistrationCount = _context.Registrations.Count(r => r.EventId == registration.EventId && r.RegistrationId != registration.RegistrationId);
            if (eventRegistrationCount >= selectedEvent.Capacity)
            {
                ModelState.AddModelError("", "The event has reached its maximum capacity.");
            }
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(registration);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(registration.RegistrationId))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        LoadDropDowns();
        return View(registration);
        
    }

    // GET: REGISTRATIONS/Delete/5
    public async Task<IActionResult> Delete(int? registrationid)
    {
        if (registrationid == null)
        {
            return NotFound();
        }

        var registration = await _context.Registrations.FindAsync(registrationid);
        if (registration == null)
        {
            return NotFound();
        }

        return View(registration);
    }

    // POST: REGISTRATIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? registrationid)
    {
        var registration = await _context.Registrations.FindAsync(registrationid);
        if (registration != null)
        {
            _context.Registrations.Remove(registration);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private void LoadDropDowns()
    {
        ViewData["VolunteerId"] = new SelectList(_context.Volunteers, "VolunteerId", "FullName");
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName");
        ViewData["Statuses"] = new SelectList(new [] { "Registered", "Waitlisted", "Cancelled" });
    }

    private bool RegistrationExists(int registrationid)
    {
        return _context.Registrations.Any(e => e.RegistrationId == registrationid);
    }
}
