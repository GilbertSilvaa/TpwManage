using TpwManage.Core.Entities;

namespace TpwManage.Core.Repositories;

public interface ISellingRepository : IRepositoryBase<Selling> 
{
  Task<List<Selling>> GetByClientIdAsync(Guid clientId);
}
