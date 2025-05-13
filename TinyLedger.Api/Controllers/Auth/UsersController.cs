using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyLedger.Application.UseCases.Users;
using Microsoft.AspNetCore.Authorization;

namespace TinyLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUserByEmail), new { email = command.Email }, new { userId });
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _mediator.Send(new GetUserByEmailQuery(email));
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}