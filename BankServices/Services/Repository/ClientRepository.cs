using BankServices.Models;
using BankServices.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Repository
{
    public class ClientRepository : BaseRepository,IClientRepository
    {
        private readonly IMongoCollection<Client> _clients;

   
        public ClientRepository(IConfiguration config):base(config)
        {
            _clients = database.GetCollection<Client>("Client");
        }
        public async Task<List<Client>> GetAllClients()
        {
            var clients =await _clients.FindSync(acc=>true).ToListAsync();
            return clients;
        }

        public async Task CreateClient(Client client)
        {
            
                var clients = new Client
                {
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    JoinDate = DateTime.Now,
                };
                await _clients.InsertOneAsync(clients);
        }

        public async Task<Client> GetClients(string ClientId)
        {
            var objId = new ObjectId(ClientId);
            var client =await _clients.Find<Client>(c => c.ClientId == objId).FirstOrDefaultAsync();
            return client;
        }
    }
}
