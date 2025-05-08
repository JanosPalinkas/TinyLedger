using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyLedger.Application.UseCases;
using TinyLedger.Domain;

namespace TinyLedger.Api.Controllers;

[ApiController]
[Route("api/accounts/{accountId}")]
public class LedgerController : ControllerBase
{
    private readonly IMediator _mediator;

    public LedgerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("transactions")]
    public async Task<IActionResult> RecordTransaction(string accountId, [FromBody] RecordTransactionRequest request)
    {
        var command = new RecordTransactionCommand(request.Amount, request.Type, request.Description)
        {
            AccountId = accountId
        };

        await _mediator.Send(command);

        return CreatedAtAction(nameof(GetTransactionHistory), new { accountId }, null);
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance(string accountId)
    {
        var balance = await _mediator.Send(new GetBalanceQuery(accountId));
        return Ok(balance);
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactionHistory(string accountId)
    {
        var transactions = await _mediator.Send(new GetTransactionHistoryQuery(accountId));
        return Ok(transactions);
    }
}

public class RecordTransactionRequest
{
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string Description { get; set; } = string.Empty;
}