using TpwManage.Core.Entities;

namespace TpwManage.Core.Repositories;

public interface IClientRepository : IRepositoryBase<Client>
{
  Task<bool> ExistsAsync(string name);
}
