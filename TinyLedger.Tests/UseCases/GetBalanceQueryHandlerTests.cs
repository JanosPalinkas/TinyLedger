using Moq;
using TinyLedger.Application.UseCases;
using TinyLedger.Domain;

namespace TinyLedger.Tests.UseCases;

public class GetBalanceQueryHandlerTests
{
    private readonly Mock<ILedgerRepository> _repositoryMock;
    private readonly GetBalanceQueryHandler _handler;

    public GetBalanceQueryHandlerTests()
    {
        _repositoryMock = new Mock<ILedgerRepository>();
        _handler = new GetBalanceQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Balance_For_Given_Account()
    {
        // Arrange
        string accountId = "account-123";
        decimal expectedBalance = 250.0m;

        _repositoryMock
            .Setup(repo => repo.GetBalance(accountId))
            .ReturnsAsync(expectedBalance);

        var query = new GetBalanceQuery(accountId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedBalance, result);
        _repositoryMock.Verify(repo => repo.GetBalance(accountId), Times.Once);
    }
}