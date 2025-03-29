using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
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
            var model = _paymentRepository.GetAllPayments();
            return View(model);
        }


        public IActionResult AddPayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _paymentRepository.AddPayment(payment);
                _paymentRepository.Save();
                return RedirectToAction("Index", "Payments");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditPayment(int PaymentId)
        {
            Payment model = _paymentRepository.GetPaymentById(PaymentId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPayment(Payment model)
        {
            if (ModelState.IsValid)
            {
                _paymentRepository.UpdatePayment(model);
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
            Payment model = _paymentRepository.GetPaymentById(PaymentID);
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
