using CarRentalApp_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp_MVC.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly RentalContext _context;

        public CarRepository(RentalContext context)
        {
            _context = context;
        }

        public IQueryable<Car> GetAll()
        {
            return _context.Cars;
        }
        public Car GetById(int CarID)
        {
            return _context.Cars.Find(CarID);
        }
        public void Insert(Car car)
        {
            _context.Cars.Add(car);
        }
        public void Update(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
        }
        public void Delete(int CarID)
        {
            Car car = _context.Cars.Find(CarID);

            if(car != null)
            {
                _context.Cars.Remove(car);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
