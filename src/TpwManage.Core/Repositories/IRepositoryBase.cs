using TpwManage.Core.Entities;

namespace TpwManage.Core;

public interface IRepositoryBase<T> where T : EntityBase
{
  Task<List<T>> GetAllAsync();
  Task<T?> GetByIdAsync(Guid id);
  Task<T> CreateAsync(T item);
  Task<T?> UpdateAsync(T item);
  Task<bool> DeleteAsync(Guid id);
}
