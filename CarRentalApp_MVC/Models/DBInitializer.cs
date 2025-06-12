using static CarRentalApp_MVC.Models.Car;
using static CarRentalApp_MVC.Models.Client;
using static CarRentalApp_MVC.Models.Payment;

namespace CarRentalApp_MVC.Models
{
    public static class DBInitializer
    {
        public static void Initialize(RentalContext context)
        {
            context.Database.EnsureCreated();

            if(context.Cars.Any())
            {
                return;
            }

            var cars = new Car[]
            {
                new Car{Brand="Toyota", CarModel="Corolla", YearOfProduction=2020, RegistrationNumber="ABC123", Status=Enum.Parse<CarStatus>("Available"), PricePerDay=40, EngineCapacity=1.8, EnginePower=140},
                new Car{Brand="Ford", CarModel="Focus", YearOfProduction=2019, RegistrationNumber="XYZ789", Status=Enum.Parse < CarStatus >("Rented"), PricePerDay=35, EngineCapacity=2.0, EnginePower=150},
                new Car{Brand="BMW", CarModel="X5", YearOfProduction=2021, RegistrationNumber="BMW555", Status=Enum.Parse<CarStatus>("Available"), PricePerDay=80, EngineCapacity=3.0, EnginePower=300},
                new Car{Brand="Audi", CarModel="A4", YearOfProduction=2018, RegistrationNumber="AUDI999", Status=Enum.Parse < CarStatus >("Available"), PricePerDay=50, EngineCapacity=2.0, EnginePower=190},
                new Car{Brand="Honda", CarModel="Civic", YearOfProduction=2022, RegistrationNumber="HONDA22",Status=Enum.Parse<CarStatus>("Rented"), PricePerDay=45, EngineCapacity=1.5, EnginePower=130}
            };
            foreach(var car in cars)
            {
                context.Cars.Add(car);
            }
            context.SaveChanges();

            var clients = new Client[]
            {
                new Client{FirstName="John", LastName="Doe", DriversLicenseNumber="DL123456", PhoneNumber="123-456-789", Email="john.doe@example.com", Status=Enum.Parse<ClientStatus>("Active")},
                new Client{FirstName="Emily", LastName="Smith", DriversLicenseNumber="DL789123", PhoneNumber="987-654-321", Email="emily.smith@example.com",Status=Enum.Parse<ClientStatus>("Active")},
                new Client{FirstName="Michael", LastName="Brown", DriversLicenseNumber="DL456789", PhoneNumber="456-123-789", Email="michael.brown@example.com", Status=Enum.Parse<ClientStatus>("Inactive")}
            };
            foreach (var client in clients)
            {
                context.Clients.Add(client);
            }
            context.SaveChanges();

            var rentals = new Rental[]
            {
                new Rental{CarId=2, ClientId=1, StartDate=DateTime.Parse("2024-03-01"), EndDate=DateTime.Parse("2024-03-10"), TotalCost=350},
                new Rental{CarId=5, ClientId=2, StartDate=DateTime.Parse("2024-02-15"), EndDate=DateTime.Parse("2024-02-20"), TotalCost=225},
                new Rental{CarId=3, ClientId=1, StartDate=DateTime.Parse("2024-01-05"), EndDate=DateTime.Parse("2024-01-12"), TotalCost=560},
                new Rental{CarId=1, ClientId=3, StartDate=DateTime.Parse("2024-03-10"), EndDate=DateTime.Parse("2024-03-15"), TotalCost=200}
            };
            foreach (var rental in rentals)
            {
                context.Rentals.Add(rental);
            }
            context.SaveChanges();

            var payments = new Payment[]
            {
                new Payment{RentalId=1, Amount=350, PaymentDate=DateTime.Parse("2024-03-01"), PayMethod=Enum.Parse<PaymentMethod>("CreditCard")},
                new Payment{RentalId=2, Amount=225, PaymentDate=DateTime.Parse("2024-02-15"), PayMethod=Enum.Parse<PaymentMethod>("PayPal")},
                new Payment{RentalId=3, Amount=560, PaymentDate=DateTime.Parse("2024-01-05"), PayMethod=Enum.Parse < PaymentMethod >("BankTransfer")},
                new Payment{RentalId=4, Amount=200, PaymentDate=DateTime.Parse("2024-03-10"), PayMethod=Enum.Parse < PaymentMethod >("Cash")}
            };
            foreach (var payment in payments)
            {
                context.Payments.Add(payment);
            }
            context.SaveChanges();
        }
    }
}
