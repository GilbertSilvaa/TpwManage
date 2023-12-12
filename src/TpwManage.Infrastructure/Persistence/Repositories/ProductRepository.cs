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

  public async Task<Product> CreateAsync(Product product)
  {
    try 
    {
      _dataSet.Add(product);
      await _context.SaveChangesAsync();
      return product;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public async Task<Product?> UpdateAsync(Product product)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(c => c.Id.Equals(product.Id));
      if(response == null) return null;

      _context.Entry(response).CurrentValues.SetValues(product);
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

  public async Task<bool> ExistsAsync(string name, string color)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(c => 
        c.Name.Equals(name) && c.Color.Equals(color)); 
        
      return response != null;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }
}
