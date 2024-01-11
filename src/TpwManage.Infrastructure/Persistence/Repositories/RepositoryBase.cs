using Microsoft.EntityFrameworkCore;
using TpwManage.Core;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;

namespace TpwManage.Infrastructure;

public abstract class RepositoryBase<T>(MyContext context) : 
  IRepositoryBase<T> where T : EntityBase
{
  protected readonly MyContext _context = context;
  protected readonly DbSet<T> _dataSet = context.Set<T>();

  public virtual async Task<List<T>> GetAllAsync()
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

  public virtual async Task<T?> GetByIdAsync(Guid id)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(r => r.Id.Equals(id));
      return response;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public virtual async Task<T> CreateAsync(T item)
  {
    try 
    {
      _dataSet.Add(item);
      await _context.SaveChangesAsync();
      return item;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public virtual async Task<T?> UpdateAsync(T item)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(r => r.Id.Equals(item.Id));
      if(response == null) return null;

      _context.Entry(response).CurrentValues.SetValues(item);
      await _context.SaveChangesAsync();
      return response;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public virtual async Task<bool> DeleteAsync(Guid id)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(r => r.Id.Equals(id));
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
