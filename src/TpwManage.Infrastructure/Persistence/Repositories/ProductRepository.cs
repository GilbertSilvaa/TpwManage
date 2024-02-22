using Microsoft.EntityFrameworkCore;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Dtos;

namespace TpwManage.Infrastructure.Persistence.Repositories;

public class ProductRepository(MyContext context) : 
  RepositoryBase<Product>(context), 
  IProductRepository
{
  public override async Task<List<Product>> GetAllAsync()
  {
    return await DapperContext.ExecuteQueryAsync<Product>($"SELECT * FROM Products");
  }

  public override async Task<Product?> GetByIdAsync(Guid id)
  {
    var response = await DapperContext
      .ExecuteQueryAsync<Product>($"SELECT * FROM Products WHERE Id = '{id}'");

    return response.FirstOrDefault();
  }

  public async Task<List<Product>> GetBySellingIdAsync(Guid sellingId)
  {
    var response = await DapperContext
      .ExecuteQueryAsync<SellingProductDto>($"SELECT * FROM ProductSelling WHERE SellingId = '{sellingId}'");

    List<Product> result = [];

    foreach (var item in response)
      result.Add(await GetByIdAsync(item.ProductsId) ?? new());

    return result;
  }

  public async Task<bool> ExistsAsync(string name, string color)
  {
    try 
    {
      var response = await _dataSet.SingleOrDefaultAsync(r => 
        r.Name.Equals(name) && r.Color.Equals(color)); 
        
      return response is not null;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }
}
