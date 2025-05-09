using Moq;
using TinyLedger.Application.UseCases.Transactions;
using TinyLedger.Domain;
using System.ComponentModel.DataAnnotations;

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

    [Fact]
    public async Task Should_Throw_ValidationException_When_Withdraw_Exceeds_Balance()
    {
        // Arrange
        var command = new RecordTransactionCommand(150, TransactionType.Withdraw, "Overdraw")
        {
            AccountId = "test-account"
        };

        _repositoryMock.Setup(r => r.GetBalance("test-account"))
            .ReturnsAsync(100);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
