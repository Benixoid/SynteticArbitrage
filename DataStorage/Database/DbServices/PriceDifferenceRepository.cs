using AutoMapper;
using DataStorage.Database.Entity;
using DataStorage.Models.DTO;

namespace DataStorage.Database.DbServices
{
    public class PriceDifferenceRepository : GenericRepository<PriceDifference, PriceDifferenceDTO>, IPriceDifferencesRepository
    {
        private readonly ILogger<PriceDifferenceRepository> _logger;
        public PriceDifferenceRepository(AppDbContext context, ILogger<PriceDifferenceRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
            _logger = logger;
        }
    }
}
