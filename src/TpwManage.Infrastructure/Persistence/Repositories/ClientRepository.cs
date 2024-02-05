using Microsoft.EntityFrameworkCore;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence.Context;

namespace TpwManage.Infrastructure.Persistence.Repositories;

public class ClientRepository(MyContext context) : 
  RepositoryBase<Client>(context), 
  IClientRepository 
{
  public async Task<bool> ExistsAsync(string name)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(r => r.Name.Equals(name)); 
      return response is not null;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }
}
