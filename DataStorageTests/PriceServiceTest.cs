using DataStorage.Database.DbServices;
using DataStorage.Models.DTO;
using DataStorage.Models;
using DataStorage.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DataStorageTests
{
    [TestFixture]
    public class PriceServiceTest
    {
        private PriceService cut;
        private Mock<ILogger<PriceService>> _mockLogger;
        private Mock<IDataManager> _mockDataManager;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<PriceService>>();
            _mockDataManager = new Mock<IDataManager>();

            cut = new PriceService(_mockDataManager.Object, _mockLogger.Object);
        }
        [Test]
        public async Task SavePriceDifferenceAsync_ValidInput_ReturnsCreatedDTO()
        {
            // Arrange
            var input = new PriceDifInput
            {
                Symbol = "AAPL",
                PriceDif = 1.23M
            };
            var expectedDto = new PriceDifferenceDTO
            {
                Symbol = input.Symbol,
                Timestamp = DateTime.UtcNow,
                Difference = input.PriceDif
            };

            _mockDataManager.Setup(dm => dm.PriceDifferences.CreateAsync(It.IsAny<PriceDifferenceDTO>()))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await cut.SavePriceDifferenceAsync(input);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Symbol, Is.EqualTo(expectedDto.Symbol));
            Assert.That(result.Difference, Is.EqualTo(expectedDto.Difference));            
        }

        [Test]
        public void SavePriceDifferenceAsync_NullSymbol_ThrowsArgumentNullException()
        {
            // Arrange
            var input = new PriceDifInput
            {
                Symbol = null,
                PriceDif = 1.23M
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await cut.SavePriceDifferenceAsync(input));
            Assert.That(ex.ParamName, Is.EqualTo("Symbol"));
        }

    }
}
