
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class PatientController : Controller
{
    private readonly HospitalDbContext _context;

    public PatientController(HospitalDbContext context)
    {
        _context = context;
    }

    // GET: Patient (With Search)
    public async Task<IActionResult> Index(string searchString)
    {
        var patients = from p in _context.Patients select p;

        if (!string.IsNullOrEmpty(searchString))
        {
            patients = patients.Where(p => p.Name.Contains(searchString));
        }

        return View(await patients.ToListAsync());
    }

    // GET: Patient/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var patient = await _context.Patients.FirstOrDefaultAsync(m => m.PatientId == id);
        if (patient == null) return NotFound();

        return View(patient);
    }

    // GET: Patient/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Patient/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PatientModel patient)
    {
        if (ModelState.IsValid)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Appointment");
        }
        return View(patient);
    }

    // GET: Patient/Edit/
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        return View(patient);
    }

    // POST: Patient/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PatientModel patient)
    {
        if (id != patient.PatientId) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View(patient);
    }

    // GET: Patient/Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        return View(patient);
    }

    // POST: Patient/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

        
    }
}