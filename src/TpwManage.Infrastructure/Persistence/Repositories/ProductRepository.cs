using Microsoft.EntityFrameworkCore;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence.Context;

namespace TpwManage.Infrastructure.Persistence.Repositories;

public class ProductRepository(MyContext context) : IProductRepository
{
  protected readonly MyContext _context = context;
  private readonly DbSet<Product> _dataSet = context.Set<Product>();

  public async Task<List<Product>> GetAllAsync()
  {
    try 
    {
      var response = await _dataSet.ToListAsync();
      return response;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public async Task<Product?> GetByIdAsync(Guid id)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(c => c.Id.Equals(id));
      return response;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public async Task<Product> CreateAsync(Product Product)
  {
    try 
    {
      _dataSet.Add(Product);
      await _context.SaveChangesAsync();
      return Product;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public async Task<Product?> UpdateAsync(Product Product)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(c => c.Id.Equals(Product.Id));
      if(response == null) return null;

      _context.Entry(response).CurrentValues.SetValues(Product);
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
