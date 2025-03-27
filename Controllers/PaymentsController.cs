using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    public class PaymentsController : Controller
    {
        private IPaymentRepository _paymentRepository;

        public PaymentsController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _paymentRepository.GetAll();
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
                _paymentRepository.Insert(payment);
                _paymentRepository.Save();
                return RedirectToAction("Index", "Payments");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditPayment(int PaymentId)
        {
            Payment model = _paymentRepository.GetById(PaymentId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPayment(Payment model)
        {
            if (ModelState.IsValid)
            {
                _paymentRepository.Update(model);
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
            Payment model = _paymentRepository.GetById(PaymentID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int PaymentID)
        {
            _paymentRepository.Delete(PaymentID);
            _paymentRepository.Save();
            return RedirectToAction("Index", "Payments");
        }
    }
}
