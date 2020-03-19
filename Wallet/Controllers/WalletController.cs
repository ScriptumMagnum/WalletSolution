using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        [HttpPost("[action]")]
        public JsonResult Deposit([FromServices] WalletService walletService, [FromQuery] long userId, [FromQuery] int currencyId, [FromQuery] decimal sum)
        {
            walletService.Deposit(userId, currencyId, sum);
            return new JsonResult("Ok");
        }

        [HttpPost("[action]")]
        public JsonResult Withdraw([FromServices] WalletService walletService, [FromQuery] long userId, [FromQuery] int currencyId, [FromQuery] decimal sum)
        {
            walletService.Withdraw(userId, currencyId, sum);
            return new JsonResult("Ok");
        }

        [HttpPost("[action]")]
        public JsonResult Exchange([FromServices] WalletService walletService, [FromQuery] long userId, [FromQuery] int sourceCurrencyId, [FromQuery] int destinationCurrencyId, [FromQuery] decimal sumInSourceCurrency)
        {
            walletService.Exchange(userId, sourceCurrencyId, destinationCurrencyId, sumInSourceCurrency);
            return new JsonResult("Ok");
        }

        [HttpGet("[action]")]
        public JsonResult GetWalletStatement([FromServices] WalletService walletService, [FromQuery] long userId)
        {
            var records = walletService.GetWalletStatement(userId);
            return new JsonResult(records);
        }
    }
}