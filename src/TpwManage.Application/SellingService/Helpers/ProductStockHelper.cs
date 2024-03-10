using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;

namespace TpwManage.Application;

internal sealed class ProductStockHelper(IProductRepository repository)
{
  private readonly IProductRepository _repository = repository;

  public async Task<bool> AdjustStock(Product product, int amount) 
  {
    try
    {
      if (product.Amount == 0) return false;
      product.Amount += amount;

      var response = await _repository.UpdateAsync(product);
      return response is not null;
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
