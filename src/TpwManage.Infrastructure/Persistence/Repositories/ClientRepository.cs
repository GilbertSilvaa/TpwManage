using Microsoft.EntityFrameworkCore;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence.Context;

namespace TpwManage.Infrastructure.Persistence.Repositories;

public class ClientRepository(MyContext context) : IClientRepository
{
  protected readonly MyContext _context = context;
  private readonly DbSet<Client> _dataSet = context.Set<Client>();

  public async Task<List<Client>> GetAllAsync()
  {
    try 
    {
      var response = await _dataSet.ToListAsync();
      return response;
    }
    catch (Exception ex) 
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<Client?> GetByIdAsync(Guid id)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(c => c.Id.Equals(id));
      return response;
    }
    catch (Exception ex) 
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<Client> CreateAsync(Client client)
  {
    try 
    {
      _dataSet.Add(client);
      await _context.SaveChangesAsync();
      return client;
    }
    catch (Exception ex) 
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<Client?> UpdateAsync(Client client)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(c => c.Id.Equals(client.Id));
      if(response == null) return null;

      _context.Entry(response).CurrentValues.SetValues(client);
      await _context.SaveChangesAsync();
      return response;
    }
    catch (Exception ex) 
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<bool> DeleteAsync(Guid id)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(c => c.Id.Equals(id));
      if(response == null) return false;

      _dataSet.Remove(response);
      await _context.SaveChangesAsync();
      return true;
    }
    catch (Exception ex) 
    {
      throw new Exception(ex.Message);
    }
  }
}
