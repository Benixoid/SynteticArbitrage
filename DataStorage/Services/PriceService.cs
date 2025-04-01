
using DataStorage.Controllers;
using DataStorage.Database.DbServices;
using DataStorage.Database.Entity;
using DataStorage.Models;
using DataStorage.Models.DTO;

namespace DataStorage.Services
{
    public class PriceService : IPriceService
    {
        private readonly ILogger<PriceService> _logger;
        public IDataManager dataManager { get; }        
        public PriceService(IDataManager dataManager, ILogger<PriceService> logger)
        {
            this.dataManager = dataManager;
            _logger = logger;
        }

        public async Task<PriceDifferenceDTO> SavePriceDifferenceAsync(PriceDifInput data)
        {
            _logger.LogInformation($"Starting saving price difference for symbol: {data.Symbol}");
            if (string.IsNullOrWhiteSpace(data.Symbol))
            {
                throw new ArgumentNullException(nameof(data.Symbol));
            }
            var dto = new PriceDifferenceDTO()
            { 
                Symbol = data.Symbol,
                Difference = data.PriceDif
            };
            _logger.LogInformation($"Saving price difference for symbol = {dto.Symbol} with difference = {dto.Difference}");
            var createdDTO = await dataManager.PriceDifferences.CreateAsync(dto);
            return createdDTO;
        }

        public async Task<PriceDTO> SavePriceAsync(PriceInput data)
        {
            _logger.LogInformation($"Starting saving price for symbol: {data.Symbol}");
            if (string.IsNullOrWhiteSpace(data.Symbol))
            {
                throw new ArgumentNullException(nameof(data.Symbol));
            }
            DateTime dateTimeToSave = data.PriceDate.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(data.PriceDate, DateTimeKind.Utc)
                : data.PriceDate.ToUniversalTime();
            var dto = new PriceDTO()
            {
                Symbol = data.Symbol,
                CurrentPrice = data.CurrentPrice,
                PriceDate = dateTimeToSave
            };
            _logger.LogInformation($"Saving price for symbol = {dto.Symbol} with price = {dto.CurrentPrice}");
            var createdDTO = await dataManager.Prices.CreateAsync(dto);
            return createdDTO;
        }
    }
}
