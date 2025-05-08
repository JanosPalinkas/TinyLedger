using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TinyLedger.Tests.Integration;

[Trait("Category", "Integration")]
public class LedgerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public LedgerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Can_Record_And_Retrieve_Transaction()
    {
        var accountId = "integration-test";

        var transaction = new
        {
            amount = 150.0m,
            type = "Deposit",
            description = "Test deposit"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(transaction),
            Encoding.UTF8,
            "application/json"
        );

        var postResponse = await _client.PostAsync($"/api/accounts/{accountId}/transactions", content);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"/api/accounts/{accountId}/transactions");
        getResponse.EnsureSuccessStatusCode();

        var body = await getResponse.Content.ReadAsStringAsync();
        Assert.Contains("Test deposit", body);
    }

    [Fact]
    public async Task Can_Get_Balance()
    {
        var accountId = "integration-test-balance";

        var transaction = new
        {
            amount = 200.0m,
            type = "Deposit",
            description = "Test deposit"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(transaction),
            Encoding.UTF8,
            "application/json"
        );

        await _client.PostAsync($"/api/accounts/{accountId}/transactions", content);

        var response = await _client.GetAsync($"/api/accounts/{accountId}/balance");
        response.EnsureSuccessStatusCode();

        var balanceJson = await response.Content.ReadAsStringAsync();
        Assert.Contains("200", balanceJson);
    }
}