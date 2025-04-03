using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    public class PaymentsController : Controller
    {
        private IPaymentService _paymentRepository;

        public PaymentsController(IPaymentService paymentRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var payments = _paymentRepository.GetAllPayments();
            var model = payments.Select(payment => new PaymentViewModel
            {
                PaymentId = payment.PaymentId,
                RentalId = payment.RentalId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
            }).ToList();
            return View(model);
        }


        public IActionResult AddPayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    PaymentId = model.PaymentId,
                    RentalId = model.RentalId,
                    Amount = model.Amount,
                    PaymentDate = model.PaymentDate,
                    PaymentMethod = model.PaymentMethod
                };
                _paymentRepository.AddPayment(payment);
                _paymentRepository.Save();
                return RedirectToAction("Index", "Payments");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditPayment(int PaymentId)
        {
            Payment payment = _paymentRepository.GetPaymentById(PaymentId);
            var model = new PaymentViewModel
            {
                PaymentId = payment.PaymentId,
                RentalId = payment.RentalId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    PaymentId = model.PaymentId,
                    RentalId = model.RentalId,
                    Amount = model.Amount,
                    PaymentDate = model.PaymentDate,
                    PaymentMethod = model.PaymentMethod
                };
                _paymentRepository.UpdatePayment(payment);
                _paymentRepository.Save();
                return RedirectToAction("Index", "Payments");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeletePayment(int PaymentID)
        {
            Payment payment = _paymentRepository.GetPaymentById(PaymentID);
            var model = new PaymentViewModel
            {
                PaymentId = payment.PaymentId,
                RentalId = payment.RentalId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int PaymentID)
        {
            _paymentRepository.DeletePayment(PaymentID);
            _paymentRepository.Save();
            return RedirectToAction("Index", "Payments");
        }
    }
}
