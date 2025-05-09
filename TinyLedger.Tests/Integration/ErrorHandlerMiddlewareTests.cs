using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TinyLedger.Api;
using Xunit;

namespace TinyLedger.Tests.Integration;

[Trait("Category", "Integration")]
public class ErrorHandlerMiddlewareTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ErrorHandlerMiddlewareTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Return_400_When_ValidationException_Is_Thrown()
    {
        var response = await _client.PostAsJsonAsync("/api/accounts/test-account/transactions", new
        {
            amount = 9999,
            type = "Withdraw",
            description = "Attempt overdraw"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains("insufficient balance", json, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_Return_500_For_Unhandled_Exception()
    {
        var response = await _client.GetAsync("/test/error/unhandled");
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {response.StatusCode} - {content}");

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
