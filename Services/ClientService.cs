using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Repository;
using Microsoft.IdentityModel.Tokens;

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
            client.PhoneNumber = FormatePhoneNumber(client.PhoneNumber);
            _clientRepository.Insert(client);
        }

        public void UpdateClient(Client client)
        {
            client.PhoneNumber = FormatePhoneNumber(client.PhoneNumber);

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

        private string FormatePhoneNumber(string phoneNumber)
        {
            if(phoneNumber.IsNullOrEmpty() || phoneNumber.Contains("-"))
            {
                return phoneNumber;
            }

            return $"{phoneNumber.Substring(0, 3)}-{phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6, 3)}";
        }
    }
}
