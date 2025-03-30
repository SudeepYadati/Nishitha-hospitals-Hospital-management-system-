using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class DoctorController : Controller
{
    private readonly HospitalDbContext _context;

    public DoctorController(HospitalDbContext context)
    {
        _context = context;
    }

    // GET: Doctor (With Search)
    public async Task<IActionResult> Index(string searchString)
    {
        var doctors = from d in _context.Doctors select d;

        if (!string.IsNullOrEmpty(searchString))
        {
            doctors = doctors.Where(d => d.Name.Contains(searchString));
        }

        return View(await doctors.ToListAsync());
    }

    // GET: Doctor/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorId == id);
        if (doctor == null) return NotFound();

        return View(doctor);
    }

    // GET: Doctor/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Doctor/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DoctorModel doctor)
    {
        if (ModelState.IsValid)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); 
        }
        return View(doctor);
    }

    // GET: Doctor/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();

        return View(doctor);
    }

    // POST: Doctor/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DoctorModel doctor)
    {
        if (id != doctor.DoctorId) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); 
        }
        return View(doctor);
    }

    // GET: Doctor/Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();

        return View(doctor);
    }

    // POST: Doctor/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id )
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();

        _context.Doctors.Remove(doctor);

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    
     
}