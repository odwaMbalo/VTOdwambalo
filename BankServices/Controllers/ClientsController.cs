using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankServices.Models;
using BankServices.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BankServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        public readonly IClientRepository _iclientRepository;

        public ClientsController(IClientRepository iclientRepository)
        {
            _iclientRepository = iclientRepository;
        }
        // GET api/values
        [HttpGet]
        public List<Client> GetAllClient()
        {
            var client = _iclientRepository.GetAllClients();
            return client;
        }

        [HttpGet("{ClientId}")]
        public async Task<Client> GetAllClient([FromRoute] Guid ClientId)
        {
            var client = await _iclientRepository.GetAllClients(ClientId);
            return client;
        }

        [HttpPost]
        public void CreateClient([FromBody]Client client)
        {
            _iclientRepository.CreateClient(client);
        }
    }
}
