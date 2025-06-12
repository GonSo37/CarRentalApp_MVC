using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Repository
{
    public interface IRentalRepository
    {
        IQueryable<Rental> GetAll();
        Rental GetById(int RentalID);
        void Insert(Rental rental);
        void Update(Rental rental);
        void Delete(int RentalID);
        void Save();
    }
}
