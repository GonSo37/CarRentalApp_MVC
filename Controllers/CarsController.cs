using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using Microsoft.AspNetCore.Mvc;

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
            var model = _carRepository.GetAllCars();
            return View(model);
        }

        
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar(Car model)
        {
            if(ModelState.IsValid)
            {
                _carRepository.AddCar(model);
                _carRepository.Save();
                return RedirectToAction("Index", "Cars");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditCar(int CarId)
        {
            Car model = _carRepository.GetCarById(CarId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(Car model)
        {
            if(ModelState.IsValid)
            {
                _carRepository.UpdateCar(model);
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
            Car model = _carRepository.GetCarById(CarID);
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
