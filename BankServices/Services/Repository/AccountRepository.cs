using BankServices.Models;
using BankServices.Services.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Repository
{
    public class AccountRepository: BaseRepository, IAccountRepository
    {
        private readonly IMongoCollection<Accounts> _accounts;
        public AccountRepository(IConfiguration config):base(config)
        {
            _accounts = database.GetCollection<Accounts>("Accounts");
        }

        public async Task<List<Accounts>> GetAccounts()
        {
            return await _accounts.Find(account=>true).ToListAsync();
        }

        public async Task<IAsyncCursor<Accounts>> GetAccounts(string id)
        {
            //var docId = new ObjectId(id);
            var accounts = await _accounts.FindAsync<Accounts>(id);
            return accounts;
        }

        public async Task<Accounts> GetAccounts(string ClientId,string AccountNumber)
        {
            var clientObjectId = new ObjectId(ClientId);
            var accounts = await _accounts.Find(acc => acc.ClientId == clientObjectId && acc.AccountNumber == AccountNumber).FirstOrDefaultAsync();
            return accounts;
        }

        public async Task<List<Accounts>> GetClientAccount(string ClientId)
        {
            var clientObjectId = new ObjectId(ClientId);
            var account = await _accounts.Find(acc => acc.ClientId == clientObjectId).ToListAsync();
            return account;
        }
        public async Task EditAccounts(Accounts accounts)
        {
            await _accounts.InsertOneAsync(accounts); 
        }

        public async Task CreateAccounts(string ClientId)
        {
            var accounts = new Accounts
            {
                AccountNumber = GenerateAccountNumber(),
                CardNumber = GenerateCardNumber(),
                OpenDate = DateTime.Now,
                ClientId = new ObjectId(ClientId)
            };
             await _accounts.InsertOneAsync(accounts); ;
        }

        public async Task DeleteAccounts(Accounts accounts)
        {
           await _accounts.InsertOneAsync(accounts);
        }

        public async Task<bool> AccountsExists(string id)
        {
           var account=  await _accounts.FindAsync<Accounts>(acc => acc.AccountNumber == id);
            return account!=null;
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
