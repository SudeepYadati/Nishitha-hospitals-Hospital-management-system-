using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


public class AppointmentController : Controller
{
    private readonly HospitalDbContext _context;

    public AppointmentController(HospitalDbContext context)
    {
        _context = context;
    }

    
    public async Task<IActionResult> Index(string searchString)
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient) //  Ensures Patient name is loaded
            .Include(a => a.Doctor)  //  Ensures Doctor name is loaded
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            appointments = appointments.Where(a => a.Patient.Name.Contains(searchString));
        }

        return View(await appointments.ToListAsync()); //  Fetches updated data correctly
    }
    // GET: Appointment/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(m => m.AppointmentId == id);

        if (appointment == null) return NotFound();

        return View(appointment);
    }

    // GET: Appointment/Create
    public IActionResult Create()
    {
        ViewData["Patients"] = _context.Patients.ToList();
        ViewData["Doctors"] = _context.Doctors.ToList();
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AppointmentModel appointment)
    {
        if (!ModelState.IsValid)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create","Billing"); 
        }

        ViewData["Patients"] = _context.Patients.ToList();
        ViewData["Doctors"] = _context.Doctors.ToList();if (!ModelState.IsValid)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View(appointment); // Returns view only if validation fails
    }
    // GET: Appointment/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        ViewData["Patients"] = _context.Patients.ToList();
        ViewData["Doctors"] = _context.Doctors.ToList();
        return View(appointment);
    }

    // POST: Appointment/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AppointmentModel appointment)
    {
        if (id != appointment.AppointmentId) return NotFound();

        if (!ModelState.IsValid)
        {
            try
            {
                _context.Update(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirects after editing
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Appointments.Any(e => e.AppointmentId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        ViewData["Patients"] = _context.Patients.ToList();
        ViewData["Doctors"] = _context.Doctors.ToList();
        return View(appointment);
    }

    // GET: Appointment/Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(m => m.AppointmentId == id);

        if (appointment == null) return NotFound();

        return View(appointment);
    }

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound(); //  Prevents null reference errors

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index)); //  Ensures redirection after deleting
    }
}