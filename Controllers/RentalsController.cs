using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    public class RentalsController : Controller
    {
        private IRentalRepository _rentalRepository;

        public RentalsController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _rentalRepository.GetAll();
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
                _rentalRepository.Insert(model);
                _rentalRepository.Save();
                return RedirectToAction("Index", "Rentals");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditRental(int RentalId)
        {
            Rental model = _rentalRepository.GetById(RentalId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditRental(Rental model)
        {
            if (ModelState.IsValid)
            {
                _rentalRepository.Update(model);
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
            Rental model = _rentalRepository.GetById(RentalId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int RentalId)
        {
            _rentalRepository.Delete(RentalId);
            _rentalRepository.Save();
            return RedirectToAction("Index", "Rentals");
        }
    }
}
