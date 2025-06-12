using CarRentalApp_MVC.Models;

namespace CarRentalApp_MVC.Services
{
    public interface IClientService
    {
        IQueryable<Client> GetAllClients ();
        Client GetClientById (int clientId);
        void AddClient (Client client);
        void UpdateClient (Client client);
        void DeleteClient (int clientId);
        void Save();

    }
}
