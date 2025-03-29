using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Repository
{
    public interface IClientRepository
    {
        IQueryable<Client> GetAll();
        Client GetById(int ClientID);
        void Insert(Client client);
        void Update(Client client);
        void Delete(int ClientID);
        void Save();
    }
}
