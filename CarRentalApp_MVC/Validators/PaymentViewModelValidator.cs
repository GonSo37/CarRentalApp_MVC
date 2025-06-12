using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.ViewModels;
using FluentValidation;

namespace CarRentalApp_MVC.Validators
{
    public class PaymentViewModelValidator : AbstractValidator<PaymentViewModel>
    {
        private IPaymentService _paymentService;

        public PaymentViewModelValidator(IPaymentService paymentService)
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
