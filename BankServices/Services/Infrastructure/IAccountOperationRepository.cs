using BankServices.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Infrastructure
{
    public interface IAccountOperationRepository
    {
        Task Depositfunds(Accounts Account, double DepositAmount);
        Task<Accounts> GetAccount(string AccountNumber);
        Task Withdrawfunds(Accounts Account, double WithDrawalAmount);
    }
}
