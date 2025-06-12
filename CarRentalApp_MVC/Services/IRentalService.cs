using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Services
{
    public interface IRentalService
    {
        IQueryable<Rental> GetAllRentals();
        Rental GetRentalById(int RentalId);
        void AddRental(Rental Rental);
        void UpdateRental(Rental Rental);
        void DeleteRental(int RentalId);
        void Save();

        decimal TotalCost(decimal price, DateTime StartDate, DateTime EndDate );
       
    }
}
