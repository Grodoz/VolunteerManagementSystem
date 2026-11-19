
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Models;
using VolunteerManagementSystem.Data;

public class VolunteersController : Controller
{
    private readonly VolunteerManagementContext _context;

    public VolunteersController(VolunteerManagementContext context)
    {
        _context = context;
    }

    // GET: VOLUNTEERS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Volunteers.ToListAsync());
    }


    // GET: VOLUNTEERS/Details/5
    public async Task<IActionResult> Details(int? volunteerid)
    {
        if (volunteerid == null)
        {
            return NotFound();
        }

        var volunteer = await _context.Volunteers
            .FirstOrDefaultAsync(m => m.VolunteerId == volunteerid);
        if (volunteer == null)
        {
            return NotFound();
        }

        return View(volunteer);
    }

    // GET: VOLUNTEERS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VOLUNTEERS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VolunteerId,FirstName,LastName,EmailAddress,PhoneNumber")] Volunteer volunteer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(volunteer);
    }

    // GET: VOLUNTEERS/Edit/5
    public async Task<IActionResult> Edit(int? volunteerid)
    {
        if (volunteerid == null)
        {
            return NotFound();
        }

        var volunteer = await _context.Volunteers.FindAsync(volunteerid);
        if (volunteer == null)
        {
            return NotFound();
        }
        return View(volunteer);
    }

    // POST: VOLUNTEERS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? volunteerid, [Bind("VolunteerId,FirstName,LastName,EmailAddress,PhoneNumber")] Volunteer volunteer)
    {
        if (volunteerid != volunteer.VolunteerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(volunteer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(volunteer.VolunteerId))
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
        return View(volunteer);
    }

    // GET: VOLUNTEERS/Delete/5
    public async Task<IActionResult> Delete(int? volunteerid)
    {
        if (volunteerid == null)
        {
            return NotFound();
        }

        var volunteer = await _context.Volunteers
            .FirstOrDefaultAsync(m => m.VolunteerId == volunteerid);
        if (volunteer == null)
        {
            return NotFound();
        }

        return View(volunteer);
    }

    // POST: VOLUNTEERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? volunteerid)
    {
        var volunteer = await _context.Volunteers.FindAsync(volunteerid);
        if (volunteer != null)
        {
            _context.Volunteers.Remove(volunteer);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VolunteerExists(int? volunteerid)
    {
        return _context.Volunteers.Any(e => e.VolunteerId == volunteerid);
    }
}
