using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class BillingController : Controller
{
    private readonly HospitalDbContext _context;

    public BillingController(HospitalDbContext context)
    {
        _context = context;
    }

    // GET: Billing (With Search)
    public async Task<IActionResult> Index(string searchString)
    {
        var bills = _context.Bills
            .Include(b => b.Patient)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            bills = bills.Where(b => b.Patient.Name.Contains(searchString));
        }

        return View(await bills.ToListAsync());
    }

    // GET: Billing/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var bill = await _context.Bills
            .Include(b => b.Patient)
            .FirstOrDefaultAsync(m => m.BillId == id);

        if (bill == null) return NotFound();

        return View(bill);
    }

    // GET: Billing/Create
    public IActionResult Create()
    {
        ViewData["Patients"] = _context.Patients.ToList();
        return View();
    }

    // POST: Billing/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BillingModel bill)
    {
        if (ModelState.IsValid)
        {
            ViewData["Patients"] = _context.Patients.ToList();
            return View(bill);
        }
        BillingModel billing = new BillingModel()
        {
            BillId = bill.BillId,
            PatientId = bill.PatientId,
            TotalAmount = bill.TotalAmount,
            PaymentStatus = bill.PaymentStatus,
            BillDate = bill.BillDate,
            Patient = _context.Patients.Find(bill.PatientId)
        };
        _context.Bills.Add(billing);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Billing/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var bill = await _context.Bills.FindAsync(id);
        if (bill == null) return NotFound();

        ViewData["Patients"] = _context.Patients.ToList();
        return View(bill);
    }

    // POST: Billing/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BillingModel bill)
    {
        if (id != bill.BillId) return NotFound();

        if (!ModelState.IsValid)
        {
            BillingModel billing = new BillingModel()
            {
                BillId = bill.BillId,
                PatientId = bill.PatientId,
                TotalAmount = bill.TotalAmount,
                PaymentStatus = bill.PaymentStatus,
                BillDate = bill.BillDate,
                Patient = _context.Patients.Find(bill.PatientId)
            };

            _context.Update(billing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["Patients"] = _context.Patients.ToList();
        return View(bill);
    }

    // GET: Billing/Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var bill = await _context.Bills
            .Include(b => b.Patient)
            .FirstOrDefaultAsync(m => m.BillId == id);

        if (bill == null) return NotFound();

        return View(bill);
    }

    // POST: Billing/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bill = await _context.Bills.FindAsync(id);
        if (bill == null) return NotFound();

        _context.Bills.Remove(bill);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}