using DataStorage.Database.Entity;
using DataStorage.Models.DTO;

namespace DataStorage.Database.DbServices
{
    public interface IGenericRepository<TEntity, TEntityDTO> 
        where TEntity : BaseEntity
        where TEntityDTO : BaseDTO
    {
        Task<TEntityDTO> CreateAsync(TEntityDTO entity);
        Task<TEntityDTO> UpdateAsync(TEntityDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<TEntityDTO>> GetAllAsync();
        Task<IEnumerable<TEntityDTO>> GetAllPaginatedAsync(int pageNum = 1, int pageSize = 20);
        Task<TEntityDTO?> GetByIdAsync(int id);
    }
}
