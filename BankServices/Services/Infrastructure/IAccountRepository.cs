using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Infrastructure
{
    public interface IAccountRepository
    {
        Task<List<Accounts>> GetAccounts();
        Task<IAsyncCursor<Accounts>> GetAccounts(string id);
        Task<Accounts> GetAccounts(string ClientId, string AccountNumbers);
        Task<List<Accounts>> GetClientAccount(string ClientId);
        Task<Accounts> CreateAccounts(string ClientId);
        Task EditAccounts(Accounts accounts);
        Task DeleteAccounts(Accounts accounts);
        Task<bool> AccountsExists(string id);
    }
}
