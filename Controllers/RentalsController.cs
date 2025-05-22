using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.Validators;
using CarRentalApp_MVC.ViewModels;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    [Authorize(Policy = "RequireAdminOrManager")]
    public class RentalsController : Controller
    {
        private IRentalService _rentalService;
        private RentalViewModelValidator _validator;
        private ICarService _carService;
        private IMapper _mapper;
        public RentalsController(IRentalService rentalRepository, RentalViewModelValidator validator, ICarService carService, IMapper mapper)
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
            var model = _mapper.Map<List<RentalViewModel>>(rentals);
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

                model.TotalCost = totalCost;

                var rental = _mapper.Map<Rental>(model);

                
                rental.CarId = model.CarId;
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

            var model = _mapper.Map<RentalViewModel>(rental);
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
                var carViewModel = _mapper.Map<CarViewModel>(car);
                var totalCost = _rentalService.TotalCost(car.PricePerDay, model.StartDate, model.EndDate);
                model.TotalCost = totalCost;
                model.Car = carViewModel;

                var rental = _mapper.Map<Rental>(model);

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
            var model = _mapper.Map<RentalViewModel>(rental);
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
