using DataStorage.Database.Entity;
using DataStorage.Models.DTO;

namespace DataStorage.Database.DbServices
{
    public interface IPriceDifferencesRepository : IGenericRepository<PriceDifference, PriceDifferenceDTO>
    {
    }
}
