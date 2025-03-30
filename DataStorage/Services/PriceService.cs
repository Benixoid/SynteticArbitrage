
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
                Timestamp = DateTime.UtcNow, 
                Difference = data.PriceDif
            };
            _logger.LogInformation($"Saving price difference for symbol = {dto.Symbol} with difference = {dto.Difference}");
            await dataManager.PriceDifferences.CreateAsync(dto);
            return dto;
        }
    }
}
