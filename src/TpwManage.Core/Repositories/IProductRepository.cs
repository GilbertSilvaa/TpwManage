using TpwManage.Core.Entities;

namespace TpwManage.Core.Repositories;

public interface IProductRepository
{
  Task<List<Product>> GetAllAsync();
  Task<Product?> GetByIdAsync(Guid id);
  Task<Product> CreateAsync(Product product);
  Task<Product?> UpdateAsync(Product product);
  Task<bool> DeleteAsync(Guid id);
  Task<bool> ExistsAsync(string name);
}
