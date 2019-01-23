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
        private string clientId { get; set; }

        private ClientsController clientControllor { get; set; }
        private AccountsController accountController { get; set; }
        [SetUp]
        public void Setup()
        {
            _clientRepository = new ClientRepository(new IConfigurationMock());
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

            clientId = clients[0].ClientId.ToString();
            Assert.IsNotNull(clientId);

        }
        [Test]
        public async Task GetClientById()
        {
            var clients = await clientControllor.GetAllClient();
            Assert.True(clients.Count > 0);

            clientId = clients[0].ClientId.ToString();
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

            clientId = clients[0].ClientId.ToString();
            _accountRepository = new AccountRepository(new IConfigurationMock());
            accountController = new AccountsController(_accountRepository,_clientRepository);
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

        [Test]
        public async Task GetClientAccountsByClientId()
        {
            var clients = await clientControllor.GetAllClient();
            Assert.True(clients.Count > 0);

            clientId = clients[0].ClientId.ToString();
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

        public string GeneratRandomAlphabets()
        {
            Random random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(8).ToArray());
        }
    }
}

