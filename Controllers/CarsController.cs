using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.Validators;
using CarRentalApp_MVC.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace CarRentalApp_MVC.Controllers
{
    public class CarsController : Controller
    {
        private ICarService _carRepository;
        private CarViewModelValidator _validator = new CarViewModelValidator();
        public CarsController(ICarService carRepository)
       {
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var cars = _carRepository.GetAllCars();
            var model = cars.Select(car => new CarViewModel
            {
                CarId = car.CarId,
                Brand = car.Brand,
                CarModel = car.CarModel,
                YearOfProduction = car.YearOfProduction,
                RegistrationNumber = car.RegistrationNumber,
                Status = car.Status,
                PricePerDay = car.PricePerDay,
                EngineCapacity = car.EngineCapacity,
                EnginePower = car.EnginePower
            }).ToList();
            return View(model);
        }

        
        public IActionResult AddCar()
        {
            return View(new CarViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCar(CarViewModel model)
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
                var car = new Car
                {
                    CarId = model.CarId,
                    Brand = model.Brand,
                    CarModel = model.CarModel,
                    YearOfProduction = model.YearOfProduction,
                    RegistrationNumber = model.RegistrationNumber,
                    Status = model.Status,
                    PricePerDay = model.PricePerDay,
                    EngineCapacity = model.EngineCapacity,
                    EnginePower = model.EnginePower
                };
                _carRepository.AddCar(car);
                _carRepository.Save();
                return RedirectToAction("Index", "Cars");
            }
     

            return View(model);
        }

        [HttpGet]
        public ActionResult EditCar(int CarId)
        {

            var car = _carRepository.GetCarById(CarId);
            var model = new CarViewModel
            {
                CarId = car.CarId,
                Brand = car.Brand,
                CarModel = car.CarModel,
                YearOfProduction = car.YearOfProduction,
                RegistrationNumber = car.RegistrationNumber,
                Status = car.Status,
                PricePerDay = car.PricePerDay,
                EngineCapacity = car.EngineCapacity,
                EnginePower = car.EnginePower
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(CarViewModel model)
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
                var car = new Car
                {
                    CarId = model.CarId,
                    Brand = model.Brand,
                    CarModel = model.CarModel,
                    YearOfProduction = model.YearOfProduction,
                    RegistrationNumber = model.RegistrationNumber,
                    Status = model.Status,
                    PricePerDay = model.PricePerDay,
                    EngineCapacity = model.EngineCapacity,
                    EnginePower = model.EnginePower
                };
                _carRepository.UpdateCar(car);
                _carRepository.Save();
                return RedirectToAction("Index", "Cars"); 
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteCar(int CarID)
        {
            if(CarID == 0 )
            {
                return RedirectToAction("Index", "Cars");
            }
            var car = _carRepository.GetCarById(CarID);
            var model = new CarViewModel
            {
                CarId = car.CarId,
                Brand = car.Brand,
                CarModel = car.CarModel,
                YearOfProduction = car.YearOfProduction,
                RegistrationNumber = car.RegistrationNumber,
                Status = car.Status,
                PricePerDay = car.PricePerDay,
                EngineCapacity = car.EngineCapacity,
                EnginePower = car.EnginePower
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int CarID)
        {
            var car = _carRepository.GetCarById(CarID);
            if ( car == null)
            {
                return NotFound();
            }
            if(car.Status == Car.CarStatus.Rented)
            {
                ModelState.AddModelError("", "Car cannot be deleted while it is rented.");
                var model = new CarViewModel
                {
                    CarId = car.CarId,
               
                };
                return RedirectToAction("DeleteCar", "Cars", new {CarID = CarID});
            }
            _carRepository.DeleteCar(CarID);
            _carRepository.Save();
            return RedirectToAction("Index", "Cars");
        }

    }
}
