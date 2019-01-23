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
    public class WithdrawalController : Controller
    {
        private readonly IAccountOperationRepository _iaccountOperationRepository;
        public WithdrawalController(IAccountOperationRepository iaccountOperationRepository)
        {
            _iaccountOperationRepository = iaccountOperationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> WithDrawFunds([FromQuery(Name = "accountNumber")]string AccountNumber, [FromQuery(Name = "withdrawalAmount")] double WithdrawalAmount)
        {
            var acount = await _iaccountOperationRepository.GetAccount(AccountNumber);
            if (acount == null)
            {
                return BadRequest(new { Message = "Failed to withdraw funds, the account does not exitis" });
            }
            await _iaccountOperationRepository.Withdrawfunds((Accounts)acount, WithdrawalAmount);
            return Ok(acount);
        }
    }
}
