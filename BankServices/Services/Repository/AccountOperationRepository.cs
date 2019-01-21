using BankServices.Models;
using BankServices.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Repository
{
    public class AccountOperationRepository : IAccountOperationRepository
    {
        private readonly ClientContext _clientContext;

        public AccountOperationRepository(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task Depositfunds(Accounts Account,double DepositAmount)
        {
            double tempBalance = Account.Balance + DepositAmount;
            Account.Balance = tempBalance;
            await _clientContext.SaveChangesAsync();
        }

        public async Task<Accounts> GetAccount(string AccountNumber)
        {
            var account = await _clientContext.Accounts.FindAsync(AccountNumber);
            return account;
        }

        public async Task Withdrawfunds(Accounts Account, double WithDrawalAmount)
        {
            double tempBalance = Account.Balance - WithDrawalAmount;
            Account.Balance = tempBalance;
            await _clientContext.SaveChangesAsync();
        }
    }
}
