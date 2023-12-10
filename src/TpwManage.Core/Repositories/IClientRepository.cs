using TpwManage.Core.Entities;

namespace TpwManage.Core.Repositories;

public interface IClientRepository
{
  Task<List<Client>> GetAllAsync();
  Task<Client?> GetByIdAsync(Guid id);
  Task<Client> CreateAsync(Client client);
  Task<Client?> UpdateAsync(Client client);
  Task<bool> DeleteAsync(Guid id);
  Task<bool> ExistsAsync(string name);
}
