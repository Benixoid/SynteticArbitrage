using DataStorage.Database.DbServices;
using DataStorage.Database.Entity;
using DataStorage.Models;
using DataStorage.Models.DTO;

namespace DataStorage.Services
{
    public interface IPriceService
    {
        public IDataManager dataManager { get; }       
        
        Task<PriceDifferenceDTO> SavePriceDifferenceAsync(PriceDifInput data);
        Task<PriceDTO> SavePriceAsync(PriceInput data);
    }
}
