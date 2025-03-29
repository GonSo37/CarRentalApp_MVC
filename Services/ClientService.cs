using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;

namespace CarRentalApp_MVC.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IQueryable<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }

        public Client GetClientById(int clientId)
        {
            return _clientRepository.GetById(clientId);
        }

        public void AddClient(Client client)
        {
            _clientRepository.Insert(client);
        }

        public void UpdateClient(Client client)
        {
            _clientRepository.Update(client);
        }

        public void DeleteClient(int clientId)
        {
            _clientRepository.Delete(clientId);
        }

        public void Save()
        {
            _clientRepository.Save();
        }
    }
}
