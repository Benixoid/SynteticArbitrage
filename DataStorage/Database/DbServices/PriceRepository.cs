using AutoMapper;
using DataStorage.Database.Entity;
using DataStorage.Models.DTO;

namespace DataStorage.Database.DbServices
{
    public class PriceRepository : GenericRepository<Price, PriceDTO>, IPriceRepository
    {
        private readonly ILogger<PriceRepository> _logger;
        public PriceRepository(AppDbContext context, ILogger<PriceRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
            _logger = logger;
        }
    }
}
