using MediatR;
using TpwManage.Core.Entities;
using TpwManage.Core;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.SellingService.Commands.UpdateSelling;

internal class UpdateSellingCommandHandler(
  ISellingRepository sellingRepository,
  IProductRepository productRepository)
  : IRequestHandler<UpdateSellingCommand, UpdateSellingResponse>
{
  private readonly ISellingRepository _sellingRepository = sellingRepository;
  private readonly IProductRepository _productRepository = productRepository;

  public async Task<UpdateSellingResponse> Handle(
    UpdateSellingCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var selling = await _sellingRepository.GetByIdAsync(request.Id)
        ?? throw new SellingNotFoundException();

      foreach (var product in selling.Products)
        await ChangeAmountProductStock(product, 1);

      List<Product> productList = [];
      foreach (var productId in request.ProductsId)
      {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) continue;

        var stockExists = await ChangeAmountProductStock(product, -1);
        if (stockExists) productList.Add(product);
      }

      Selling sellingUpdate = new(selling.Client)
      {
        Id = selling.Id,
        CreateAt = selling.CreateAt
      };

      sellingUpdate.SetupProducts(productList);

      var response = await _sellingRepository.UpdateAsync(sellingUpdate);
      return UpdateSellingResponse.FromEntity(response!);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  private async Task<bool> ChangeAmountProductStock(Product product, int amount)
  {
    try
    {
      if (product.Amount == 0) return false;

      product.Amount += amount;
      var response = await _productRepository.UpdateAsync(product);
      return response is not null;
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
