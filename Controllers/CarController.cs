using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    public class CarController : Controller
    {
        private ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
       {
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _carRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCar(Car model)
        {
            if(ModelState.IsValid)
            {
                _carRepository.Insert(model);
                _carRepository.Save();
                return RedirectToAction("Index", "Car");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditCar(int CarId)
        {
            Car model = _carRepository.GetById(CarId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCar(Car model)
        {
            if(ModelState.IsValid)
            {
                _carRepository.Update(model);
                _carRepository.Save();
                return RedirectToAction("Index", "Car"); 
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteCar(int CarID)
        {
            Car model = _carRepository.GetById(CarID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int CarID)
        {
            _carRepository.Delete(CarID);
            _carRepository.Save();
            return RedirectToAction("Index", "Car");
        }

    }
}
