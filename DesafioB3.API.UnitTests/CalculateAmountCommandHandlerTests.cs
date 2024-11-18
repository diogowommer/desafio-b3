namespace DesafioB3.API.UnitTests
{
    using Xunit;
    using System.Threading;
    using System.Threading.Tasks;
    using DesafioB3.API.Features.CalculateAmount;

    public class CalculateAmountCommandHandlerTests
    {
        private readonly CalculateAmountCommandHandler _handler;

        public CalculateAmountCommandHandlerTests()
        {
            _handler = new CalculateAmountCommandHandler();
        }

        [Theory]
        [InlineData(1000, 6, 1059.76, 1046.31)] // Test case for 6 months
        [InlineData(1000, 12, 1123.08, 1098.47)] // Test case for 12 months
        [InlineData(1000, 24, 1261.31, 1215.58)] // Test case for 24 months
        [InlineData(1000, 36, 1416.56, 1354.07)] // Test case for 36 months
        public async Task CalculateAmount_ReturnsExpectedValues(decimal initialValue, int months, decimal expectedGrossValue, decimal expectedNetValue)
        {
            // Arrange
            var command = new CalculateAmountCommand(initialValue, months);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedGrossValue, Math.Round(response.GrossAmount, 2));
            Assert.Equal(expectedNetValue, Math.Round(response.NetAmount, 2));
        }

        [Fact]
        public void CalculateFinalValue_CalculatesCorrectly()
        {
            // Arrange
            decimal initialValue = 1000;
            int months = 6;

            // Act
            decimal result = CalculateAmountCommandHandler.CalculateFinalValue(initialValue, months);

            // Assert
            Assert.Equal(1059.76m, Math.Round(result, 2));
        }

        [Theory]
        [InlineData(6, 0.225)]  // Tax rate for 6 months
        [InlineData(12, 0.20)]  // Tax rate for 12 months
        [InlineData(24, 0.175)] // Tax rate for 24 months
        [InlineData(36, 0.15)]  // Tax rate for 36 months
        public void GetTaxRate_ReturnsCorrectRate(int months, decimal expectedRate)
        {
            // Act
            var result = typeof(CalculateAmountCommandHandler)
                .GetMethod("GetTaxRate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                .Invoke(null, [months]);

            Assert.NotNull(result);

            decimal taxRate = (decimal)result!;

            // Assert
            Assert.Equal(expectedRate, taxRate);
        }
    }
}