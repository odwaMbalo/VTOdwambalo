using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Infrastructure
{
    public interface IAccountRepository
    {
        IEnumerable<Accounts> GetAccounts();
        Task<Accounts> GetAccounts(int id);
        Task<Accounts> GetAccounts(Guid ClientId, string id);
        Task<List<Accounts>> GetAccounts(Guid ClientId);
        Task CreateAccounts(Guid ClientId);
        Task EditAccounts(Accounts accounts);
        Task DeleteAccounts(Accounts accounts);
        bool AccountsExists(string id);
    }
}
