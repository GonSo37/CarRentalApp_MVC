using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.ViewModels;
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
            var rentals = _rentalRepository.GetAllRentals();
            var model = rentals.Select(rental => new RentalViewModel
            {
                RentalId = rental.RentalId,
                CarId = rental.CarId,
                ClientId = rental.ClientId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                TotalCost = rental.TotalCost,
                Car = rental.Car,
                Client = rental.Client

            }).ToList();

            return View(model);
        }


        public IActionResult AddRental()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRental(RentalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rental = new Rental
                {
                    RentalId = model.RentalId,
                    CarId = model.CarId,
                    ClientId = model.ClientId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TotalCost = model.TotalCost,
                    Car = model.Car,
                    Client = model.Client
                };
                _rentalRepository.AddRental(rental);
                _rentalRepository.Save();
                return RedirectToAction("Index", "Rentals");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditRental(int RentalId)
        {
            Rental rental = _rentalRepository.GetRentalById(RentalId);
            var model = new RentalViewModel
            {
                RentalId = rental.RentalId,
                CarId = rental.CarId,
                ClientId = rental.ClientId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                TotalCost = rental.TotalCost,
                Car = rental.Car,
                Client = rental.Client
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditRental(RentalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rental = new Rental
                {
                    RentalId = model.RentalId,
                    CarId = model.CarId,
                    ClientId = model.ClientId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TotalCost = model.TotalCost,
                    Car = model.Car,
                    Client = model.Client
                };
                _rentalRepository.UpdateRental(rental);
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
            Rental rental = _rentalRepository.GetRentalById(RentalId);
            var model = new RentalViewModel
            {
                RentalId = rental.RentalId,
                CarId = rental.CarId,
                ClientId = rental.ClientId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                TotalCost = rental.TotalCost,
                Car = rental.Car,
                Client = rental.Client
            };
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
