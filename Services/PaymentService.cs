using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;

namespace CarRentalApp_MVC.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public IQueryable<Payment> GetAllPayments()
        {
            return _paymentRepository.GetAll();
        }

        public Payment GetPaymentById(int paymentId)
        {
            return _paymentRepository.GetById(paymentId);
        }

        public void AddPayment(Payment payment)
        {
            _paymentRepository.Insert(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            _paymentRepository.Update(payment);
        }

        public void DeletePayment(int paymentId)
        {
            _paymentRepository.Delete(paymentId);
        }

        public void Save()
        {
            _paymentRepository.Save();
        }
    }
}
