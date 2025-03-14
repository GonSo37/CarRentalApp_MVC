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
        public async Task<ActionResult> Create([Bind("ClientId, FirstName, LastName, DriversLicenseNumber, PhoneNumber, Email, Status")] Client client)
        {
            if(ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return View(nameof(Index));
            }
            return View(client);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }
    }
}
