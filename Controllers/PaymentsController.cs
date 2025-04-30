using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.Validators;
using CarRentalApp_MVC.ViewModels;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp_MVC.Controllers
{
    public class PaymentsController : Controller
    {
        private IPaymentService _paymentRepository;
        private PaymentViewModelValidator _validator;
        private IMapper _mapper;
        public PaymentsController(IPaymentService paymentRepository, PaymentViewModelValidator validator, IMapper mapper)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _validator = validator;
            _mapper = mapper; 
        }

        [HttpGet]
        public ActionResult Index()
        {
            var payments = _paymentRepository.GetAllPayments();
            var model = _mapper.Map<List<PaymentViewModel>>(payments);

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
                var payment = _mapper.Map<Payment>(model);

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
            var model = _mapper.Map<PaymentViewModel>(payment);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPayment(PaymentViewModel model)
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
                var payment = _mapper.Map<Payment>(model);

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
            var model = _mapper.Map<PaymentViewModel>(payment);

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
