using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Repository
{
    public interface ICarRepository
    {
        IQueryable<Car> GetAll();
        Car GetById(int CarID);
        void Insert(Car car);
        void Update(Car car);
        void Delete(int CarID);
        void Save();
    }
}
