using Microsoft.AspNetCore.Mvc;
using TinyLedger.Application.UseCases.Balance;
using TinyLedger.Application.UseCases.Transactions;
using TinyLedger.Domain;
using MediatR;
using TinyLedger.Api.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace TinyLedger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/accounts")]
    public class LedgerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LedgerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private string? GetUserAccountId()
        {
            return User.FindFirst("accountId")?.Value;
        }

        [HttpPost("transactions")]
        public async Task<IActionResult> RecordTransaction([FromBody] RecordTransactionCommand command)
        {
            var accountId = GetUserAccountId();

            if (string.IsNullOrEmpty(accountId))
                return Unauthorized("Missing AccountId claim.");

            command.AccountId = accountId;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("balance")]
        public async Task<ActionResult<decimal>> GetBalance()
        {
            var accountId = GetUserAccountId();

            if (string.IsNullOrEmpty(accountId))
                return Unauthorized("Missing AccountId claim.");

            var result = await _mediator.Send(new GetBalanceQuery(accountId));
            return Ok(result);
        }

        [HttpGet("transactions")]
        public async Task<ActionResult<PaginatedResult<Transaction>>> GetTransactionHistory(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var accountId = GetUserAccountId();

            if (string.IsNullOrEmpty(accountId))
                return Unauthorized("Missing AccountId claim.");

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