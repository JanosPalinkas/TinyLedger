using Microsoft.AspNetCore.Mvc;
using TinyLedger.Application.UseCases.Balance;
using TinyLedger.Application.UseCases.Transactions;
using TinyLedger.Domain;
using MediatR;
using TinyLedger.Api.Dtos;

namespace TinyLedger.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class LedgerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LedgerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("accounts/{accountId}/transactions")]
        public async Task<IActionResult> RecordTransaction(string accountId, [FromBody] RecordTransactionCommand command)
        {
            command.AccountId = accountId;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("accounts/{accountId}/balance")]
        public async Task<ActionResult<decimal>> GetBalance(string accountId)
        {
            var result = await _mediator.Send(new GetBalanceQuery(accountId));
            return Ok(result);
        }

        [HttpGet("accounts/{accountId}/transactions")]
        public async Task<ActionResult<PaginatedResult<Transaction>>> GetTransactionHistory(
            string accountId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var allTransactions = await _mediator.Send(new GetTransactionHistoryQuery(accountId));

            var paginated = allTransactions
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PaginatedResult<Transaction>
            {
                Items = paginated,
                TotalItems = allTransactions.Count,
                Page = page,
                PageSize = pageSize
            };

            return Ok(result);
        }
    }
}
