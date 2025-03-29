using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Services
{
    public interface ICarService
    {
        IQueryable<Car> GetAllCars();
        Car GetCarById(int CarId);
        void AddCar(Car Car);
        void UpdateCar(Car Car);
        void DeleteCar(int CarId);
        void Save();
    }
}
