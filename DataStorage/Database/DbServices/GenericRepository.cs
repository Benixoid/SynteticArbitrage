using DataStorage.Models;
using DataStorage.Database.Entity;
using Microsoft.EntityFrameworkCore;
using DataStorage.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DataStorage.Database.DbServices
{
    public class GenericRepository<TEntity, TEntityDTO> : IGenericRepository<TEntity, TEntityDTO> 
        where TEntity : BaseEntity
        where TEntityDTO : BaseDTO
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IMapper _mapper;
        private readonly ILogger<GenericRepository<TEntity, TEntityDTO>> _logger;
        public GenericRepository(AppDbContext context, ILogger<GenericRepository<TEntity, TEntityDTO>> logger, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _logger = logger;
            _mapper = mapper;
        }
        public virtual async Task<TEntityDTO> CreateAsync(TEntityDTO createDto)
        {
            createDto.Timestamp = DateTime.UtcNow;
            var entity = _mapper.Map<TEntity>(createDto);
            _logger.LogInformation($"Saving new entity of type: {entity.GetType().Name}");
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TEntityDTO>(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<TEntityDTO>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TEntityDTO>>(entities);            
        }

        public virtual async Task<IEnumerable<TEntityDTO>> GetPaginatedAsync(int pageNum = 1, int pageSize = 20)
        {
            var entities = await _dbSet.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map<IEnumerable<TEntityDTO>>(entities);
        }

        public virtual async Task<TEntityDTO?> GetByIdAsync(int id)
        {
            var entity = await _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<TEntityDTO>(entity);
        }

        public virtual async Task<TEntityDTO> UpdateAsync(TEntityDTO updateDTO)
        {
            var entity = _mapper.Map<TEntity>(updateDTO);
            _logger.LogInformation($"Updating entity of type: {entity.GetType().Name}");
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TEntityDTO>(entity);
        }
    }
}
