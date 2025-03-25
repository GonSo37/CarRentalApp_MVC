using CarRentalApp_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp_MVC.Controllers
{
    public class ClientsController : Controller
    {
        private readonly RentalContext _context;

        public ClientsController(RentalContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
             Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                           "Try again, and if the problem persists " +
                           "see your system administrator.");
            }
            
            return View(client);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
                return NotFound();

            var clientToUpdate = _context.Clients.FirstOrDefault(s => s.ClientId == id);

            if(await TryUpdateModelAsync<Client>(
                clientToUpdate,
                "",
                c => c.FirstName, c => c.LastName, c => c.PhoneNumber, c => c.DriversLicenseNumber, c => c.Email, c => c.Status))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException )
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }

            return View(clientToUpdate);

        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
                return NotFound();
            
            var client = await _context.Clients.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ClientId == id);
            
            if (client == null)
                return NotFound();

            if(saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                   "Delete failed. Try again, and if the problem persists " +
                   "see your system administrator.";
            }

            return View(client);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Clients.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Clients.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException )
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(s => s.Rentals)
                    .ThenInclude(e => e.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }
    }
}
