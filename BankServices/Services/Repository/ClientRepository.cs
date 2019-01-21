using BankServices.Models;
using BankServices.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _clientContext;

        public ClientRepository(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }
        public List<Client> GetAllClients()
        {
            var clients =  _clientContext.Clients.Select(c=>c).ToList();
            return clients;
        }
        public void CreateClient(Client client)
        {
            
                var clients = new Client
                {
                    ClientId = Guid.NewGuid(),
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Level = 1,
                    JoinDate = DateTime.Now
                };

                _clientContext.Clients.Add(clients);
                _clientContext.SaveChanges();
        }

        public async Task<Client> GetAllClients(Guid ClientId)
        {
            return await _clientContext.Clients.FindAsync(ClientId);
        }
    }
}
