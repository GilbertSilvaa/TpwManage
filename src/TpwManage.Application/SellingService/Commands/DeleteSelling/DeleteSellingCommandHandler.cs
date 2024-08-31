using MediatR;
using TpwManage.Core;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.SellingService.Commands.DeleteSelling;

internal class DeleteSellingCommandHandler(
  ISellingRepository sellingRepository,
  IProductRepository productRepository)
  : IRequestHandler<DeleteSellingCommand, bool>
{
  private readonly ISellingRepository _sellingRepository = sellingRepository;
  private readonly ProductStockHelper _productStockHelper = new(productRepository);

  public async Task<bool> Handle(
    DeleteSellingCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var selling = await _sellingRepository.GetByIdAsync(request.Id) 
        ?? throw new SellingNotFoundException();

      foreach (var product in selling.Products)
        await _productStockHelper.AdjustStock(product, +1);

      return await _sellingRepository.DeleteAsync(request.Id);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
