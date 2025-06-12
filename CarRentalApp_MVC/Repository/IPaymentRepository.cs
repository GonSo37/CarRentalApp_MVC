using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Repository
{
    public interface IPaymentRepository
    {
        IQueryable<Payment> GetAll();
        Payment GetById(int PaymentID);
        void Insert(Payment payment);
        void Update(Payment payment);
        void Delete(int PaymentID);
        void Save();
    }
}
