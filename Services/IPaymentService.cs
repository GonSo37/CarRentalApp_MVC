using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Services
{
    public interface IPaymentService
    {
        IQueryable<Payment> GetAllPayments();
        Payment GetPaymentById(int paymentId);
        void AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int paymentId);
        void Save();
    }
}
