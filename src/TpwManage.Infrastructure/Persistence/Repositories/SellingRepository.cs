using Microsoft.EntityFrameworkCore;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence.Context;

namespace TpwManage.Infrastructure.Persistence.Repositories;

public class SellingRepository(MyContext context) : 
  RepositoryBase<Selling>(context), 
  ISellingRepository
{
  private readonly ClientRepository _clientRepository = new(context);
  private readonly ProductRepository _productRepository = new(context);

  public override async Task<List<Selling>> GetAllAsync()
  {
    try 
    {
      return await LoadClientProductsInSellings(await base.GetAllAsync());
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
      var response = await LoadClientProductsInSellings([await base.GetByIdAsync(id) ?? new()]);

      return response.FirstOrDefault();
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }

  public async Task<List<Selling>> GetByClientIdAsync(Guid clientId)
  {
    try
    {
      var response = await _dapper
        .ExecuteQueryAsync<Selling>($@"SELECT * FROM Sellings WHERE ClientId = '{clientId}'");

      return await LoadClientProductsInSellings(response);
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
      if (response == null) return null;

      response.Client = selling.Client;
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

  private async Task<List<Selling>> LoadClientProductsInSellings(List<Selling> sellings)
  {
    List<Selling> sellingsWithClientProducts = [];

    foreach (var s in sellings)
    {
      s.Client = await _clientRepository.GetByIdAsync(s.ClientId) ?? new();
      s.ClearProducts();
      s.SetupProducts(await _productRepository.GetBySellingIdAsync(s.Id));
      sellingsWithClientProducts.Add(s);
    };

    return sellingsWithClientProducts;
  }
}
