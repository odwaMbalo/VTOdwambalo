using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankServices.Models;
using BankServices.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BankServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        public readonly IClientRepository _iclientRepository;
        public readonly IAccountRepository _accountRepository;

        public ClientsController(IClientRepository iclientRepository,IAccountRepository accountRepository)
        {
            _iclientRepository = iclientRepository;
            _accountRepository = accountRepository;
        }
        // GET api/values
        [HttpGet]
        public async Task<List<Client>> GetAllClient()
        {
            var client = await _iclientRepository.GetAllClients();
            return client;
        }

        [HttpGet("{ClientId}")]
        public async Task<IActionResult> GetAllClient([FromRoute] string ClientId)
        {
            var objectClientId = new ObjectId(ClientId);
            var client = await _iclientRepository.GetClients(ClientId);
            if (client==null)
            {
                return BadRequest(new { massage="Client with that id does not exist"});
            }
            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody]Client client)
        {
            _iclientRepository.CreateClient(client);
            return Ok(client);
        }


    }
}
