using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.Validators;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    [Authorize(Policy = "RequireAdminOrManager")]
    public class RentalsController : Controller
    {
        private IRentalService _rentalService;
        private RentalModelValidator _validator;
        private ICarService _carService;
        private IMapper _mapper;
        public RentalsController(IRentalService rentalRepository, RentalModelValidator validator, ICarService carService, IMapper mapper)
        {
            _rentalService = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            _validator = validator;
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var rentals = _rentalService.GetAllRentals();
            return View(rentals);
        }


        public IActionResult AddRental()
        {
            return View();
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRental(Rental rental)
        {
            var result = _validator.Validate(rental);

            if(!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }


            if (ModelState.IsValid)
            {
                var car = _carService.GetCarById(rental.CarId);
                var totalCost = _rentalService.TotalCost(car.PricePerDay, rental.StartDate, rental.EndDate);

                rental.TotalCost = totalCost;
                
                rental.CarId = rental.CarId;
                rental.Car = null;

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

            return View(rental);
        }

        [HttpPost]
        public ActionResult EditRental(Rental rental)
        {

            var result = _validator.Validate(rental);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                var car = _carService.GetCarById(rental.CarId);
                var carViewModel = _mapper.Map<Car>(car);
                var totalCost = _rentalService.TotalCost(car.PricePerDay, rental.StartDate, rental.EndDate);
                rental.TotalCost = totalCost;
                rental.Car = carViewModel;


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
            return View(rental);
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
