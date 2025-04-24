using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.Validators;
using CarRentalApp_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    public class RentalsController : Controller
    {
        private IRentalService _rentalService;
        private RentalViewModelValidator _validator;
        private ICarService _carService;
        public RentalsController(IRentalService rentalRepository, RentalViewModelValidator validator, ICarService carService)
        {
            _rentalService = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            _validator = validator;
            _carService = carService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var rentals = _rentalService.GetAllRentals();
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
            var result = _validator.Validate(model);

            if(!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            

            if (ModelState.IsValid)
            {
                var car = _carService.GetCarById(model.CarId);
                var totalCost = _rentalService.TotalCost(car.PricePerDay, model.StartDate, model.EndDate);

                var rental = new Rental
                {
                    RentalId = model.RentalId,
                    CarId = model.CarId,
                    ClientId = model.ClientId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TotalCost = totalCost,
                    Car = model.Car,
                    Client = model.Client
                };
                _rentalService.AddRental(rental);
                _rentalService.Save();
                return RedirectToAction("Index", "Rentals");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditRental(int RentalId)
        {
            Rental rental = _rentalService.GetRentalById(RentalId);
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

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                var car = _carService.GetCarById(model.CarId);
                var totalCost = _rentalService.TotalCost(car.PricePerDay, model.StartDate, model.EndDate);
                var rental = new Rental
                {
                    RentalId = model.RentalId,
                    CarId = model.CarId,
                    ClientId = model.ClientId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TotalCost = totalCost,
                    Car = model.Car,
                    Client = model.Client
                };
                _rentalService.UpdateRental(rental);
                _rentalService.Save();
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
            Rental rental = _rentalService.GetRentalById(RentalId);
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
            _rentalService.DeleteRental(RentalId);
            _rentalService.Save();
            return RedirectToAction("Index", "Rentals");
        }
    }
}
