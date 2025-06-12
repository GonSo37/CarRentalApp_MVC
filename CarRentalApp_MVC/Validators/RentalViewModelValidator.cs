using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.ViewModels;
using FluentValidation;

namespace CarRentalApp_MVC.Validators
{
    public class RentalViewModelValidator : AbstractValidator<RentalViewModel>
    {
        private IRentalService _rentalService;

        public RentalViewModelValidator(IRentalService rentalService)
        {
            _rentalService = rentalService;

            RuleFor(x => x.CarId)
                .NotEmpty().WithMessage("Car ID is required.")
                .GreaterThan(0).WithMessage("Rental ID must be greater than 0");

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Client ID is required.")
                .GreaterThan(0).WithMessage("Rental ID must be greater than 0");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start Date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End Date is required.")
                .GreaterThan(r => r.StartDate).WithMessage("End date must after Start date");
                    

        }



    }
}
