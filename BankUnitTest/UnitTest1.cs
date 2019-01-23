using BankServices.Controllers;
using BankServices.Models;
using BankServices.Services.Infrastructure;
using BankServices.Services.Repository;
using NUnit.Framework;
using System;
using Microsoft.Extensions.Configuration;
using BankUnitTest;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        private IAccountRepository _accountRepository { get; set; }
        private IClientRepository _clientRepository { get; set; }
        private IAccountOperationRepository _iaccoutOperationRepository { get; set; }
        private string clientId { get; set; }

        private ClientsController clientControllor { get; set; }
        private AccountsController accountController { get; set; }
        private DepositController depositController { get; set; }
        private WithdrawalController withdrawalController { get; set; }
        [SetUp]
        public void Setup()
        {
            _clientRepository = new ClientRepository(new IConfigurationMock());
            _accountRepository = new AccountRepository(new IConfigurationMock());
            _iaccoutOperationRepository = new AccountOperationRepository(new IConfigurationMock());

            accountController = new AccountsController(_accountRepository, _clientRepository);
            depositController = new DepositController(_iaccoutOperationRepository);
            withdrawalController = new WithdrawalController(_iaccoutOperationRepository);


        }

        [Test]
        public async Task CreatClient()
        {

             clientControllor = new ClientsController(_clientRepository, _accountRepository);
             var createdClient = await clientControllor.CreateClient(
                new Client {
                    FirstName  = GeneratRandomAlphabets(),
                    LastName = GeneratRandomAlphabets(),
                    JoinDate = DateTime.Now,
                });

            Assert.IsInstanceOf(typeof(OkObjectResult), createdClient);
            
        }
        [Test]
        public async Task GetAllClient()
        {
            var clients = await clientControllor.GetAllClient();
            Assert.True(clients.Count > 0);
            int max = clients.Count - 1;

            int index = rand.Next(0,max);
            clientId = clients[index].ClientId.ToString();
            Assert.IsNotNull(clientId);
        }

        [Test]
        public async Task GetClientById()
        {
            var clients = await clientControllor.GetAllClient();
            Assert.True(clients.Count > 0);
            int max = clients.Count - 1;

            int index = rand.Next(0, max);

            clientId = clients[index].ClientId.ToString();
            var client = await clientControllor.GetAllClient(clientId);
            Assert.IsInstanceOf(typeof(OkObjectResult), client);
            
        }

        [Test]
        public async Task GetNoneExistingClientById()
        {
            var client = await clientControllor.GetAllClient("000000000000000000000000");
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), client);
        }

        [Test]
        public async Task CreatClientAccount()
        {
            var clients = await clientControllor.GetAllClient();
            Assert.True(clients.Count > 0);
            int max = clients.Count - 1;
            int index = rand.Next(0, max);

            clientId = clients[index].ClientId.ToString();
            var creationResult = await accountController.CreateAccounts(clientId);
            Assert.IsInstanceOf(typeof(CreatedAtActionResult), creationResult);
        }

        [Test]
        public async Task CreateAccountForNoneExistingClient()
        {
            var id = "000000000000000000000000";
            var creationResult = await accountController.CreateAccounts(id);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), creationResult);
        }

        [Test]
        public async Task GetAllAccounts()
        {
            List<Accounts> acounts = await accountController.GetAccounts();
            Assert.IsTrue(acounts.Count>0);
        }
        private Random rand = new Random();
        [Test]
        public async Task GetClientAccountsByClientId()
        {
            var clients = await clientControllor.GetAllClient();
            Assert.True(clients.Count > 0);
            int max = clients.Count - 1;
            int index = rand.Next(0, max);

            clientId = clients[index].ClientId.ToString();
            
            var creationResult = await accountController.CreateAccounts(clientId);
            Assert.IsInstanceOf(typeof(CreatedAtActionResult), creationResult);

            var acounts = await accountController.GetClientAccount(clientId);
            Assert.IsInstanceOf(typeof(OkObjectResult), acounts);
        }

        [Test]
        public async Task GetClientAccountsByWrondClienId()
        {
            var id = "000000000000000000000000";
            var acounts = await accountController.GetClientAccount(id);
            Assert.IsInstanceOf(typeof(NotFoundResult), acounts);
        }

        [Test]
        public async Task DepositFund()
        {
            var clients = await clientControllor.GetAllClient();
            Assert.True(clients.Count > 0);
            int max = clients.Count - 1;
            int index = rand.Next(0, max);

            clientId = clients[index].ClientId.ToString();

            var creationResult = await accountController.CreateAccounts(clientId);
            Assert.IsInstanceOf(typeof(CreatedAtActionResult), creationResult);
            var accountToDeposit = (creationResult as CreatedAtActionResult).Value as Accounts;
            
            var deposits = await depositController.DepositFunds(accountToDeposit.AccountNumber,823.5);
            Assert.IsInstanceOf(typeof(OkObjectResult), deposits);

            var accountToDeposited = (deposits as OkObjectResult).Value as Accounts;
            Assert.IsTrue(accountToDeposited.Balance==823.5);
        }

        [Test]
        public async Task WithDrawFund()
        {
            var clients = await clientControllor.GetAllClient();
            Assert.True(clients.Count > 0);
            int max = clients.Count - 1;
            int index = rand.Next(0, max);

            clientId = clients[index].ClientId.ToString();

            var creationResult = await accountController.CreateAccounts(clientId);
            Assert.IsInstanceOf(typeof(CreatedAtActionResult), creationResult);
            var accountToDeposit = (creationResult as CreatedAtActionResult).Value as Accounts;

            var deposits = await depositController.DepositFunds(accountToDeposit.AccountNumber, 823.5);
            Assert.IsInstanceOf(typeof(OkObjectResult), deposits);

            var accountToDeposited = (deposits as OkObjectResult).Value as Accounts;
            Assert.IsTrue(accountToDeposited.Balance == 823.5);

            var withdraw = await withdrawalController.WithDrawFunds(accountToDeposited.AccountNumber,23.00);
            Assert.IsInstanceOf(typeof(OkObjectResult), withdraw);
            var accountToWithdraw = (withdraw as OkObjectResult).Value as Accounts;
            Assert.IsTrue(accountToWithdraw.Balance==800.5);
        }

        public string GeneratRandomAlphabets()
        {
            Random random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(8).ToArray());
        }
    }
}

