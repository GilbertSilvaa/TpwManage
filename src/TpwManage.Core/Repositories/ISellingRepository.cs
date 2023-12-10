using TpwManage.Core.Entities;

namespace TpwManage.Core.Repositories;

public interface ISellingRepository
{
  Task<List<Selling>> GetAllAsync();
  Task<Selling?> GetByIdAsync(Guid id);
  Task<Selling> CreateAsync(Selling selling);
  Task<Selling?> UpdateAsync(Selling selling);
  Task<bool> DeleteAsync(Guid id);
}
