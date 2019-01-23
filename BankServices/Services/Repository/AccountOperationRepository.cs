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
    public class AccountOperationRepository : BaseRepository, IAccountOperationRepository
    {

        private readonly IMongoCollection<Accounts> _accounts;
        public AccountOperationRepository(IConfiguration config) : base(config)
        {
            _accounts = database.GetCollection<Accounts>("Accounts");
        }
       
        public async Task Depositfunds(Accounts account,double DepositAmount)
        {
            double tempBalance = account.Balance + DepositAmount;
            account.Balance = tempBalance;
            var accountNum = account.AccountNumber;
            var updoneresult = await _accounts.ReplaceOneAsync<Accounts>(acc => acc.AccountNumber == accountNum, account);
        }

        public async Task<Accounts> GetAccount(string AccountNumber)
        {
            var account = await _accounts.Find(acc=>acc.AccountNumber== AccountNumber).FirstOrDefaultAsync();
            return account;
        }

        public async Task Withdrawfunds(Accounts Account, double WithDrawalAmount)
        {
            double tempBalance = Account.Balance - WithDrawalAmount;
            Account.Balance = tempBalance;
            var accountNum = Account.AccountNumber;
            var updoneresult = await _accounts.ReplaceOneAsync<Accounts>(acc => acc.AccountNumber == accountNum, Account);
        }
    }
}
