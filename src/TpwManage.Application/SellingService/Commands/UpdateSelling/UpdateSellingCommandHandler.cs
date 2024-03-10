using MediatR;
using TpwManage.Core.Entities;
using TpwManage.Core;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.SellingService.Commands.UpdateSelling;

internal class UpdateSellingCommandHandler(
  ISellingRepository sellingRepository,
  IProductRepository productRepository)
  : IRequestHandler<UpdateSellingCommand, SellingResponse>
{
  private readonly ISellingRepository _sellingRepository = sellingRepository;
  private readonly IProductRepository _productRepository = productRepository;
  private readonly ProductStockHelper _productStockHelper = new(productRepository);

  public async Task<SellingResponse> Handle(
    UpdateSellingCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var selling = await _sellingRepository.GetByIdAsync(request.Id)
        ?? throw new SellingNotFoundException();

      foreach (var product in selling.Products)
        await _productStockHelper.AdjustStock(product, 1);

      List<Product> productList = [];
      foreach (var productId in request.ProductsId)
      {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) continue;

        var stockExists = await _productStockHelper.AdjustStock(product, -1);
        if (stockExists) productList.Add(product);
      }

      Selling sellingUpdate = new(selling.Client)
      {
        Id = selling.Id,
        CreateAt = selling.CreateAt
      };

      sellingUpdate.SetupProducts(productList);

      var response = await _sellingRepository.UpdateAsync(sellingUpdate);
      return SellingResponse.FromEntity(response!);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
