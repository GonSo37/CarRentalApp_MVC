using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.Validators;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace CarRentalApp_MVC.Controllers
{
    [Authorize(Policy = "RequireAdminOrManager")]
    public class CarsController : Controller
    {
        private ICarService _carService;
        private CarModelValidator _validator;
        private IMapper _mapper;
        public CarsController(ICarService carService, CarModelValidator validator, IMapper mapper)
       {
            _carService = carService ?? throw new ArgumentNullException(nameof(carService));
            _validator = validator;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            var cars = _carService.GetAllCars();

            var model = _mapper.Map<List<Car>>(cars);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddCar()
        {
            return View(new Car());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar(Car car)
        {

            var result = _validator.Validate(car);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
           
                _carService.AddCar(car);
                _carService.Save();
                return RedirectToAction("Index", "Cars");
            }
     

            return View(car);
        }

        [HttpGet]
        public ActionResult EditCar(int CarId)
        {

            var car = _carService.GetCarById(CarId);
            if(car == null)
            {
                return NotFound();
            }


            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(Car car)
        {
            var result = _validator.Validate(car);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {

                _carService.UpdateCar(car);
                _carService.Save();
                return RedirectToAction("Index", "Cars"); 
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
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

            return View(car);
        }

        [Authorize(Roles = "Admin")]
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
