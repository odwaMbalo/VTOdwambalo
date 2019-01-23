using BankServices.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Infrastructure
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClients(string ClientId);
        Task CreateClient(Client client);
    }
}
