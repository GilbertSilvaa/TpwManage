using TpwManage.Core.Entities;

namespace TpwManage.Core.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
  Task<List<Product>> GetBySellingIdAsync(Guid sellingId);
  Task<bool> ExistsAsync(string name, string color);
}
