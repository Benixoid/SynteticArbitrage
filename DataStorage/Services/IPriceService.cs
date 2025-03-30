using DataStorage.Database.DbServices;
using DataStorage.Database.Entity;
using DataStorage.Models;

namespace DataStorage.Services
{
    public interface IPriceService
    {
        public IDataManager dataManager { get; }       
        
        Task<PriceDifference> SavePriceDifferenceAsync(PriceDifInput data);        
    }
}
