using CarRentalApp_MVC.Services;
using FluentValidation;
using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Validators
{
    public class PaymentModelValidator : AbstractValidator<Payment>
    {
        private IPaymentService _paymentService;

        public PaymentModelValidator(IPaymentService paymentService)
        {
            _paymentService = paymentService;

            RuleFor(x => x.RentalId)
                .NotEmpty().WithMessage("Rental ID is required.")
                .GreaterThan(0).WithMessage("Rental ID must be greater than 0");

            RuleFor(x => x.Amount)
                 .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0");

            RuleFor(x => x.PaymentDate)
                .NotEmpty().WithMessage("Payment Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Payment Date cannot be in the future.");

          

        }
    }
}
