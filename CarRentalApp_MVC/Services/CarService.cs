using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;

namespace CarRentalApp_MVC.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IQueryable<Car> GetAllCars()
        {
            return _carRepository.GetAll();
        }

        public Car GetCarById(int carId)
        {
            return _carRepository.GetById(carId);
        }

        public void AddCar(Car car)
        {
            _carRepository.Insert(car);
        }

        public void UpdateCar(Car car)
        {
            _carRepository.Update(car);
        }

        public void DeleteCar(int carId)
        {
            _carRepository.Delete(carId);
        }

        public void Save()
        {
            _carRepository.Save();
        }
    }
}
