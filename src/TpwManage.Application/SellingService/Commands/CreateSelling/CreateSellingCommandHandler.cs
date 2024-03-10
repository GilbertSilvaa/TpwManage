using MediatR;
using TpwManage.Core.Entities;
using TpwManage.Core;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.SellingService.Commands.CreateSelling;

internal class CreateSellingCommandHandler(
  ISellingRepository sellingRepository,
  IClientRepository clientRepository,
  IProductRepository productRepository)
  : IRequestHandler<CreateSellingCommand, SellingResponse>
{
  private readonly ISellingRepository _sellingRepository = sellingRepository;
  private readonly IClientRepository _clientRepository = clientRepository;
  private readonly IProductRepository _productRepository = productRepository;
  private readonly ProductStockHelper _productStockHelper = new(productRepository);

  public async Task<SellingResponse> Handle(
    CreateSellingCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var client = await _clientRepository.GetByIdAsync(request.ClientId)
      ?? throw new ClientNotFoundException();

      List<Product> productList = [];
      foreach (var productId in request.ProductsId)
      {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) continue;

        var stockExists = await _productStockHelper.AdjustStock(product, -1);
        if (stockExists) productList.Add(product);
      }

      Selling selling = new(client);
      selling.SetupProducts(productList);

      var response = await _sellingRepository.CreateAsync(selling);
      return SellingResponse.FromEntity(response);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
