using Moq;
using TinyLedger.Application.UseCases.Transactions;
using TinyLedger.Domain;

namespace TinyLedger.Tests.UseCases;

[Trait("Category", "UnitTests")]
public class RecordTransactionCommandHandlerTests
{
    private readonly Mock<ILedgerRepository> _repositoryMock;
    private readonly RecordTransactionCommandHandler _handler;

    public RecordTransactionCommandHandlerTests()
    {
        _repositoryMock = new Mock<ILedgerRepository>();
        _handler = new RecordTransactionCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Should_Add_Transaction_To_Repository()
    {
        // Arrange
        var command = new RecordTransactionCommand(100, TransactionType.Deposit, "Salary")
        {
            AccountId = "test-account"
        };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(repo =>
            repo.AddTransaction("test-account", It.Is<Transaction>(t =>
                t.Amount == 100 &&
                t.Type == TransactionType.Deposit &&
                t.Description == "Salary"
            )), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public async Task Should_Throw_Exception_When_Amount_Is_Invalid(decimal amount)
    {
        // Arrange
        var command = new RecordTransactionCommand(amount, TransactionType.Deposit, "Invalid")
        {
            AccountId = "test-account"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }
}