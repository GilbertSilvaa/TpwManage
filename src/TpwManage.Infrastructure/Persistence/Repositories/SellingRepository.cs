using Microsoft.EntityFrameworkCore;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence.Context;

namespace TpwManage.Infrastructure.Persistence.Repositories;

public class SellingRepository(MyContext context) : ISellingRepository
{
  protected readonly MyContext _context = context;
  private readonly DbSet<Selling> _dataSet = context.Set<Selling>();

  public async Task<List<Selling>> GetAllAsync()
  {
    try 
    {
      var response = await _dataSet
        .Include(s => s.Products)
        .Include(s => s.Client)
        .ToListAsync();
        
      return response;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public async Task<Selling?> GetByIdAsync(Guid id)
  {
    try 
    {
      var response = await _dataSet
        .Include(s => s.Products)
        .Include(s => s.Client)
        .SingleOrDefaultAsync(c => c.Id.Equals(id));

      return response;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public async Task<Selling> CreateAsync(Selling selling)
  {
    try 
    {
      _dataSet.Add(selling);
      await _context.SaveChangesAsync();
      return selling;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public async Task<Selling?> UpdateAsync(Selling selling)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(c => c.Id.Equals(selling.Id));
      if(response == null) return null;

      response.ClearProducts();
      response.SetupProducts(selling.Products);

      _context.Entry(response).CurrentValues.SetValues(selling);
      await _context.SaveChangesAsync();
      return response;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
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
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }
}
