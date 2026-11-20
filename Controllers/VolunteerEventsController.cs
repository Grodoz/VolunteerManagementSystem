
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Models;
using VolunteerManagementSystem.Data;

public class VolunteerEventsController : Controller
{
    private readonly VolunteerManagementContext _context;

    public VolunteerEventsController(VolunteerManagementContext context)
    {
        _context = context;
    }

    // GET: VOLUNTEEREVENTSS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Events.ToListAsync());
    }

    // GET: VOLUNTEEREVENTSS/Details/5
    public async Task<IActionResult> Details(int? eventid)
    {
        if (eventid == null)
        {
            return NotFound();
        }

        var volunteerevents = await _context.Events
            .FirstOrDefaultAsync(m => m.EventId == eventid);
        if (volunteerevents == null)
        {
            return NotFound();
        }

        return View(volunteerevents);
    }

    // GET: VOLUNTEEREVENTSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VOLUNTEEREVENTSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EventId,EventName,EventDescription,EventDate,Location,Capacity,EventStatus,CreatedDate")] VolunteerEvents volunteerevents)
    {
        if (ModelState.IsValid)
        {
            _context.Add(volunteerevents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(volunteerevents);
    }

    // GET: VOLUNTEEREVENTSS/Edit/5
    public async Task<IActionResult> Edit(int? eventid)
    {
        if (eventid == null)
        {
            return NotFound();
        }

        var volunteerevents = await _context.Events.FindAsync(eventid);
        if (volunteerevents == null)
        {
            return NotFound();
        }
        return View(volunteerevents);
    }

    // POST: VOLUNTEEREVENTSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? eventid, [Bind("EventId,EventName,EventDescription,EventDate,Location,Capacity,EventStatus,CreatedDate")] VolunteerEvents volunteerevents)
    {
        if (eventid != volunteerevents.EventId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(volunteerevents);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerEventsExists(volunteerevents.EventId))
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
        return View(volunteerevents);
    }

    // GET: VOLUNTEEREVENTSS/Delete/5
    public async Task<IActionResult> Delete(int? eventid)
    {
        if (eventid == null)
        {
            return NotFound();
        }

        var volunteerevents = await _context.Events
            .FirstOrDefaultAsync(m => m.EventId == eventid);
        if (volunteerevents == null)
        {
            return NotFound();
        }

        return View(volunteerevents);
    }

    // POST: VOLUNTEEREVENTSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? eventid)
    {
        var volunteerevents = await _context.Events.FindAsync(eventid);
        if (volunteerevents != null)
        {
            _context.Events.Remove(volunteerevents);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VolunteerEventsExists(int? eventid)
    {
        return _context.Events.Any(e => e.EventId == eventid);
    }
}
