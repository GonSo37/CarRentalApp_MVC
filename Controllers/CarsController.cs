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
        private ICarService _carService;
        private CarViewModelValidator _validator;
        public CarsController(ICarService carService, CarViewModelValidator validator)
       {
            _carService = carService ?? throw new ArgumentNullException(nameof(carService));
            _validator = validator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var cars = _carService.GetAllCars();
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
        public ActionResult AddCar(CarViewModel model)
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
                _carService.AddCar(car);
                _carService.Save();
                return RedirectToAction("Index", "Cars");
            }
     

            return View(model);
        }

        [HttpGet]
        public ActionResult EditCar(int CarId)
        {

            var car = _carService.GetCarById(CarId);
            if(car == null)
            {
                return NotFound();
            }
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
                _carService.UpdateCar(car);
                _carService.Save();
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
            
            var car = _carService.GetCarById(CarID);
            if(car == null )
            {
                return NotFound();
            }
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
            var car = _carService.GetCarById(CarID);
            if ( car == null)
            {
                return NotFound();
            }
       
            _carService.DeleteCar(CarID);
            _carService.Save();
            return RedirectToAction("Index", "Cars");
        }

    }
}
