using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.ViewModels;
using FluentValidation;
namespace CarRentalApp_MVC.Validators
{
    public class ClientViewModelValidator : AbstractValidator<ClientViewModel>
    {
        private IClientService _clientService;

        public ClientViewModelValidator(IClientService clientService)
        {
            _clientService = clientService;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 30).WithMessage("First name must be between 2 and 100 characters long.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 30).WithMessage("First name must be between 2 and 100 characters long.");

            RuleFor(x => x.DriversLicenseNumber)
                .NotEmpty().WithMessage("Drivers License is required")
                .Length(8).WithMessage("driver's license must have 8 characters")
                .Matches(@"^[A-Za-z]{2}\d{6}$").WithMessage("The driver's license must have the first 2 letters and 6 digits (example: DL123456)");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(\d{9}|\d{3}-\d{3}-\d{3})$").WithMessage("Phone number must have 9 characters (example: 123456789)");

            RuleFor(x => x.Email)
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Incorrect format of the email");
        }

        

    }
}
