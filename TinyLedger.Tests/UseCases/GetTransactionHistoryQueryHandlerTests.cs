using Moq;
using TinyLedger.Application.UseCases;
using TinyLedger.Domain;

namespace TinyLedger.Tests.UseCases;

[Trait("Category", "UnitTests")]
public class GetTransactionHistoryQueryHandlerTests
{
    private readonly Mock<ILedgerRepository> _repositoryMock;
    private readonly GetTransactionHistoryQueryHandler _handler;

    public GetTransactionHistoryQueryHandlerTests()
    {
        _repositoryMock = new Mock<ILedgerRepository>();
        _handler = new GetTransactionHistoryQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Transaction_History_For_Given_Account()
    {
        // Arrange
        string accountId = "account-xyz";

        var expectedTransactions = new List<Transaction>
        {
            new Transaction(100, TransactionType.Deposit, "Salary"),
            new Transaction(40, TransactionType.Withdraw, "Groceries")
        };

        _repositoryMock
            .Setup(repo => repo.GetTransactionHistory(accountId))
            .ReturnsAsync(expectedTransactions);

        var query = new GetTransactionHistoryQuery(accountId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedTransactions.Count, result.Count);
        Assert.Equal(expectedTransactions[0].Amount, result[0].Amount);
        _repositoryMock.Verify(repo => repo.GetTransactionHistory(accountId), Times.Once);
    }
}