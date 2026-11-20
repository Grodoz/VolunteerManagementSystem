using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Models;
using VolunteerManagementSystem.Data;

public class RegistrationsController : Controller
{
    private readonly VolunteerManagementContext _context;

    public RegistrationsController(VolunteerManagementContext context)
    {
        _context = context;
    }

    // GET: REGISTRATIONS
    public async Task<IActionResult> Index()    
    {
        var registrations = _context.Registrations
            .Include(r => r.Volunteer)
            .Include(r => r.Event);
        return View(await registrations.ToListAsync());
    }

    // GET: REGISTRATIONS/Details/5
    public async Task<IActionResult> Details(int? registrationid)
    {
        if (registrationid == null)
        {
            return NotFound();
        }

        var registration = await _context.Registrations
            .FirstOrDefaultAsync(m => m.RegistrationId == registrationid);
        if (registration == null)
        {
            return NotFound();
        }

        return View(registration);
    }

    // GET: REGISTRATIONS/Create
    public IActionResult Create()
    {
        ViewData["VolunteerId"] = new SelectList(_context.Volunteers, "VolunteerId", "FullName");
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName");
        return View();
    }

    // POST: REGISTRATIONS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RegistrationId,VolunteerId,EventId,RegistrationDate,RegistrationStatus,Volunteer,Event")] Registration registration)
    {
        if (ModelState.IsValid)
        {
            _context.Add(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["VolunteerId"] = new SelectList(_context.Volunteers, "VolunteerId", "FirstName", registration.VolunteerId);
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", registration.EventId);
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
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(registration);
    }

    // GET: REGISTRATIONS/Delete/5
    public async Task<IActionResult> Delete(int? registrationid)
    {
        if (registrationid == null)
        {
            return NotFound();
        }

        var registration = await _context.Registrations
            .FirstOrDefaultAsync(m => m.RegistrationId == registrationid);
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

    private bool RegistrationExists(int? registrationid)
    {
        return _context.Registrations.Any(e => e.RegistrationId == registrationid);
    }
}
