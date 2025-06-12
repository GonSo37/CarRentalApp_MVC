

using CarRentalApp_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp_MVC.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RentalContext _context;

        public PaymentRepository(RentalContext context)
        {
            _context = context;
        }

        public IQueryable<Payment> GetAll()
        {
            return _context.Payments.Include(r => r.Rental);
                
        }
        public Payment GetById(int PaymentID)
        {
            return _context.Payments
            .Include(r => r.Rental)
            .FirstOrDefault(r => r.PaymentId == PaymentID);
        }
        public void Insert(Payment payment)
        {
            _context.Payments.Add(payment);
        }
        public void Update(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
        }
        public void Delete(int PaymentID)
        {
            Payment payment = _context.Payments.Find(PaymentID);

            if (payment!= null)
            {
                _context.Payments.Remove(payment);
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
