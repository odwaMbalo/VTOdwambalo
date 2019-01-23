using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankServices.Models;
using BankServices.Services.Infrastructure;
using MongoDB.Driver;

namespace BankServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private IAccountRepository _accountRepository { get; set; }
        private IClientRepository _clientRepository { get; set; }
        public AccountsController(IAccountRepository accountRepository, IClientRepository clientRepository)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<List<Accounts>> GetAccounts()
        {
            return await _accountRepository.GetAccounts();
        }

        [HttpGet("{ClientId}")]
        public async Task<IActionResult> GetClientAccount([FromRoute] string ClientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accounts = await _accountRepository.GetClientAccount(ClientId);
           
            if (accounts == null || accounts.Count<=0)
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        [HttpGet("{ClientId}/{AccountNumber}")]
        public async Task<IActionResult> GetAccounts([FromRoute] string ClientId, [FromRoute]string AccountNumber)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accounts = await _accountRepository.GetAccounts(ClientId, AccountNumber);

            if (accounts == null)
            {

                var msg = new
                {
                    Message = "Failed to find the client with that id and account number",
                    Reason = "Client Id or Account number does not exist"
                };
                return NotFound(msg);
            }
            return Ok(accounts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAccounts([FromRoute] string id, [FromBody] Accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(accounts.AccountNumber))
            {
                return BadRequest();
            }

            try
            {
                await _accountRepository.EditAccounts(accounts);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _accountRepository.AccountsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("{ClientId}")]
        public async Task<IActionResult> CreateAccounts([FromRoute] string ClientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _clientRepository.GetClients(ClientId);
            if (client==null)
            {
                var msgs = new
                {
                    Message = "Failed to creat account",
                    Reason = "Client does not exist"
                };
                return  NotFound(msgs); 
            }

            
            var acc = await _accountRepository.CreateAccounts(ClientId);
           
            return CreatedAtAction("GetAccounts", acc, acc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccounts([FromRoute] String id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accounts = await _accountRepository.GetAccounts(id); ;
            if (accounts == null)
            {
                return NotFound();
            }

            await _accountRepository.DeleteAccounts((Accounts)accounts);

            return Ok(accounts);
        }
    }
}