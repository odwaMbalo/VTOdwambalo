using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankServices.Models;
using BankServices.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankServices.Controllers
{
    [Route("api/[controller]")]
    public class DepositController : Controller
    {
        private readonly IAccountOperationRepository _iaccountOperationRepository;
        public DepositController(IAccountOperationRepository iaccountOperationRepository)
        {
            _iaccountOperationRepository = iaccountOperationRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> DepositFunds([FromQuery(Name = "accountNumber")]string AccountNumber,[FromQuery(Name = "depositAmount")] double DepositAmount)
        {
            var acount = await _iaccountOperationRepository.GetAccount(AccountNumber);
            if (acount==null)
            {
                return BadRequest(new {Message ="Failed to deposit funds, the account does not exitis" });
            }
            await _iaccountOperationRepository.Depositfunds(acount, DepositAmount);
            return Ok(acount);
        }
    }
}
