using BankServices.Models;
using BankServices.Services.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly ClientContext _clientcontext;
        
        public AccountRepository(ClientContext clientcontext)
        {
            _clientcontext = clientcontext;
        }

        public IEnumerable<Accounts> GetAccounts()
        {
            return _clientcontext.Accounts;
        }

        public async Task<Accounts> GetAccounts(int id)
        {
            var accounts = await _clientcontext.Accounts.FindAsync(id);
            return accounts;
        }

        public async Task<Accounts> GetAccounts(Guid ClientId,string id)
        {
            var accounts = await _clientcontext.Accounts.FirstOrDefaultAsync(c=>c.ClientId==ClientId && c.AccountNumber==id);
            return accounts;
        }

        public async Task<List<Accounts>> GetAccounts(Guid ClientId)
        {
            var accounts = await _clientcontext.Accounts.Where(c => c.ClientId == ClientId).ToListAsync();
            return accounts;
        }
        public async Task EditAccounts(Accounts accounts)
        {
            _clientcontext.Accounts.Add(accounts);
            await _clientcontext.SaveChangesAsync();   
        }

        public async Task CreateAccounts(Guid ClientId)
        {
            var accounts = new Accounts
            {
                AccountNumber = GenerateAccountNumber(),
                CardNumber = GenerateCardNumber(),
                OpenDate = DateTime.Now,
                ClientId = ClientId
            };
             _clientcontext.Accounts.Add(accounts);
             await _clientcontext.SaveChangesAsync();
        }

        public async Task DeleteAccounts(Accounts accounts)
        {
            _clientcontext.Accounts.Remove(accounts);
            await _clientcontext.SaveChangesAsync();
        }

        public bool AccountsExists(string id)
        {
            return _clientcontext.Accounts.Any(e => e.AccountNumber.Equals(id));
        }

        public string GenerateAccountNumber()
        {
            Random rnd = new Random();
            return string.Format("{0,-19:R}", rnd.NextDouble()).Substring(2, 10);
        }

        public string GenerateCardNumber()
        {
            Random rnd = new Random();
            return string.Format("{0,-19:R}", rnd.NextDouble()).Substring(2, 16);
        }
    }
}
