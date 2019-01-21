using BankServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Infrastructure
{
    public interface IClientRepository
    {
        List<Client> GetAllClients();
        Task<Client> GetAllClients(Guid ClientId);
        void CreateClient(Client client);
    }
}
