using CarRentalApp_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp_MVC.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalContext _context;

        public RentalRepository(RentalContext context)
        {
            _context = context;
        }

        public IQueryable<Rental> GetAll()
        {
            return _context.Rentals.Include(r => r.Car)
                .Include(r => r.Client);
        }
        public Rental GetById(int RentalID)
        {
            return _context.Rentals.Find(RentalID);
        }
        public void Insert(Rental rental)
        {
            _context.Rentals.Add(rental);
        }
        public void Update(Rental rental)
        {
            _context.Entry(rental).State = EntityState.Modified;
        }
        public void Delete(int RentalID)
        {
            Rental rental = _context.Rentals.Find(RentalID);

            if (rental != null)
            {
                _context.Rentals.Remove(rental);
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
