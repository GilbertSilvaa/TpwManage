using TpwManage.Core.Entities;

namespace TpwManage.Core.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
  Task<bool> ExistsAsync(string name, string color);
}
