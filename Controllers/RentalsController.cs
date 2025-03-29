using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    public class RentalsController : Controller
    {
        private IRentalService _rentalRepository;

        public RentalsController(IRentalService rentalRepository)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _rentalRepository.GetAllRentals();
            return View(model);
        }


        public IActionResult AddRental()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRental(Rental model)
        {
            if (ModelState.IsValid)
            {
                _rentalRepository.AddRental(model);
                _rentalRepository.Save();
                return RedirectToAction("Index", "Rentals");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditRental(int RentalId)
        {
            Rental model = _rentalRepository.GetRentalById(RentalId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditRental(Rental model)
        {
            if (ModelState.IsValid)
            {
                _rentalRepository.UpdateRental(model);
                _rentalRepository.Save();
                return RedirectToAction("Index", "Rentals");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteRental(int RentalId)
        {
            Rental model = _rentalRepository.GetRentalById(RentalId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int RentalId)
        {
            _rentalRepository.DeleteRental(RentalId);
            _rentalRepository.Save();
            return RedirectToAction("Index", "Rentals");
        }
    }
}
