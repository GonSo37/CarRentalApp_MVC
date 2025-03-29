using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;

namespace CarRentalApp_MVC.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public IQueryable<Rental> GetAllRentals()
        {
            return _rentalRepository.GetAll();
        }

        public Rental GetRentalById(int rentalId)
        {
            return _rentalRepository.GetById(rentalId);
        }

        public void AddRental(Rental rental)
        {
            _rentalRepository.Insert(rental);
        }

        public void UpdateRental(Rental rental)
        {
            _rentalRepository.Update(rental);
        }

        public void DeleteRental(int rentalId)
        {
            _rentalRepository.Delete(rentalId);
        }

        public void Save()
        {
            _rentalRepository.Save();
        }
    }
}
