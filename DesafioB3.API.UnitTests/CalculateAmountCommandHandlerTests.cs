using System.Threading;
using System.Threading.Tasks;
using DesafioB3.API.Features.CalculateAmount;
using FluentAssertions;
using Xunit;

namespace DesafioB3.Tests.Features.CalculateAmount
{
    public class CalculateAmountCommandHandlerTests
    {
        private readonly CalculateAmountCommandHandler _handler;

        public CalculateAmountCommandHandlerTests()
        {
            _handler = new CalculateAmountCommandHandler();
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectValues_ForShortTerm()
        {
            // Arrange
            var command = new CalculateAmountCommand(1000m, 6);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.GrossAmount.Should().BeApproximately(1059.75m, 0.01m); 
            result.NetAmount.Should().BeApproximately(1046.31m, 0.01m);
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectValues_ForMediumTerm()
        {
            // Arrange
            var command = new CalculateAmountCommand(1000m, 12);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.GrossAmount.Should().BeApproximately(1123.08m, 0.01m);
            result.NetAmount.Should().BeApproximately(1098.46m, 0.01m);
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectValues_ForLongTerm()
        {
            // Arrange
            var command = new CalculateAmountCommand(1000m, 24);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.GrossAmount.Should().BeApproximately(1261.31m, 0.01m);
            result.NetAmount.Should().BeApproximately(1215.58m, 0.01m);
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectValues_ForExtendedTerm()
        {
            // Arrange
            var command = new CalculateAmountCommand(1000m, 36);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.GrossAmount.Should().BeApproximately(1416.55m, 0.01m);
            result.NetAmount.Should().BeApproximately(1354.07m, 0.01m);
        }

        [Fact]
        public void CalculateGrossValue_ShouldHandleZeroMonths()
        {
            // Arrange
            var initialValue = 1000m;
            var months = 0;

            // Act
            var grossValue = CalculateAmountCommandHandler.CalculateGrossValue(initialValue, months);

            // Assert
            grossValue.Should().Be(1000m);
        }

        [Fact]
        public void DetermineTaxRate_ShouldReturnCorrectRate()
        {
            // Act & Assert
            CalculateAmountCommandHandler.DetermineTaxRate(3).Should().Be(0.225m);
            CalculateAmountCommandHandler.DetermineTaxRate(9).Should().Be(0.20m);
            CalculateAmountCommandHandler.DetermineTaxRate(18).Should().Be(0.175m);
            CalculateAmountCommandHandler.DetermineTaxRate(30).Should().Be(0.15m);
        }
    }
}