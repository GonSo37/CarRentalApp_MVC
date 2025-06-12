using CarRentalApp_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp_MVC.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly RentalContext _context;

        public ClientRepository(RentalContext context)
        {
            _context = context;
        }

        public IQueryable<Client> GetAll()
        {
            return _context.Clients;
        }
        public Client GetById(int clientID)
        {
            return _context.Clients.Find(clientID);
        }
        public void Insert(Client client)
        {
            _context.Clients.Add(client);
        }
        public void Update(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
        }
        public void Delete(int clientID)
        {
            Client client = _context.Clients.Find(clientID);

            if (client != null)
            {
                _context.Clients.Remove(client);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
