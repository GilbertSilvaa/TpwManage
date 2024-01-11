using Microsoft.EntityFrameworkCore;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence.Context;

namespace TpwManage.Infrastructure.Persistence.Repositories;

public class SellingRepository(MyContext context) : 
  RepositoryBase<Selling>(context), ISellingRepository
{
  public override async Task<List<Selling>> GetAllAsync()
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

  public override async Task<Selling?> GetByIdAsync(Guid id)
  {
    try 
    {
      var response = await _dataSet
        .Include(s => s.Products)
        .Include(s => s.Client)
        .SingleOrDefaultAsync(r => r.Id.Equals(id));

      return response;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public override async Task<Selling?> UpdateAsync(Selling selling)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(r => r.Id.Equals(selling.Id));
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
}
