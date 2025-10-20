using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.Validators;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaymentsController : Controller
    {
        private IPaymentService _paymentRepository;
        private PaymentModelValidator _validator;
        public PaymentsController(IPaymentService paymentRepository, PaymentModelValidator validator, IMapper mapper)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _validator = validator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var payments = _paymentRepository.GetAllPayments();

            return View(payments);
        }


        public IActionResult AddPayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPayment(Payment model)
        {
            var result = _validator.Validate(model);
            if(!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {

                _paymentRepository.AddPayment(model);
                _paymentRepository.Save();
                return RedirectToAction("Index", "Payments");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditPayment(int PaymentId)
        {
            Payment payment = _paymentRepository.GetPaymentById(PaymentId);
            return View(payment);
        }

        [HttpPost]
        public ActionResult EditPayment(Payment model)
        {
            var result = _validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
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
            Payment payment = _paymentRepository.GetPaymentById(PaymentID);

            return View(payment);
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
