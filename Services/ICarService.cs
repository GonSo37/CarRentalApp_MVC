using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Services
{
    public interface ICarService
    {
        Task<IQueryable<Car>> GetAllCars();
        Car GetById(int CarID);
        void Insert(Car car);
        void Update(Car car);
        void Delete(int CarID);
        void Save();
    }
}
