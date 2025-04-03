using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarRentalApp_MVC.Controllers
{
    public class CarsController : Controller
    {
        private ICarService _carRepository;

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
        public ActionResult AddCar(CarViewModel model)
        {
            if(ModelState.IsValid)
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
            return View();
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
            if(ModelState.IsValid)
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
            _carRepository.DeleteCar(CarID);
            _carRepository.Save();
            return RedirectToAction("Index", "Cars");
        }

    }
}
