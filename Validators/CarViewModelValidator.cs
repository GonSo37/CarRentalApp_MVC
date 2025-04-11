using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.ViewModels;
using FluentValidation;

namespace CarRentalApp_MVC.Validators
{
    public class CarViewModelValidator : AbstractValidator<CarViewModel>
    {
        private ICarService _carService;
        public CarViewModelValidator(ICarService carService)
        {
            _carService = carService;
            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required.")
                .Length(3, 100).WithMessage("Brand must be between 3 and 100 characters long.");

            RuleFor(x => x.CarModel)
                .NotEmpty().WithMessage("Car model is required.")
                .Length(3, 100).WithMessage("Car model must be between 3 and 100 characters long.");

            RuleFor(x => x.YearOfProduction)
                .NotNull().WithMessage("Year of Production is required.")
                .InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage($"Year of production must be between 1900 and {DateTime.Now.Year}.");

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty().WithMessage("Registration number is required.")
                .Length(3, 20).WithMessage("Registration number must be between 3 and 20 characters long.");

            RuleFor(x => x.Status)
                   .IsInEnum()
                   .WithMessage("Invalid status selected.");

            RuleFor(x => x.PricePerDay)
                .NotNull().WithMessage("Price per day is required.")
                .InclusiveBetween(1, 10000).WithMessage("Price per day must be greater than 0 and less than 10,000.");

            RuleFor(x => x.EngineCapacity)
                .NotNull().WithMessage("Engine Capacity is required.")
                .GreaterThan(0).WithMessage("Engine capacity must be greater than 0.");

            RuleFor(x => x.EnginePower)
                .NotNull().WithMessage("Engine power is required.")
                .GreaterThan(0).WithMessage("Engine power must be greater than 0.");

            RuleFor(x => x.RegistrationNumber)
                            .NotNull().WithMessage("Registration number is required.")
                            .NotEmpty().WithMessage("Registration number cannot be empty.")
                            .Must(BeUniqueRegistrationNumber).WithMessage("This registration number already exists.");
        }
        private bool BeUniqueRegistrationNumber(string registrationNumber)
        {
            return !_carService.GetAllCars().Any(car => car.RegistrationNumber == registrationNumber);
        }
    }

    
}
