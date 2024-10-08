using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Dtos;

namespace TpwManage.Infrastructure.Persistence.Repositories;

public class ProductRepository(MyContext context) : 
  RepositoryBase<Product>(context), 
  IProductRepository
{
  public async Task<List<Product>> GetBySellingIdAsync(Guid sellingId)
  {
    var response = await _dapper
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
      var response = await _dapper
        .ExecuteQueryAsync<Product>($"SELECT * FROM Products WHERE Name = '{name}' AND Color = '{color}'");

      return response.FirstOrDefault() is not null;
    }
    catch (Exception ex) 
    {
      var messageException = ex.InnerException?.Message ?? ex.Message;
      throw new Exception(messageException);
    }
  }
}
