using MediatR;
using TpwManage.Core;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ProductService.Commands.UpdateProduct;

internal class UpdateProductCommandHandler(IProductRepository repository)
  : IRequestHandler<UpdateProductCommand, ProductResponse>
{
  private readonly IProductRepository _repository = repository;

  public async Task<ProductResponse> Handle(
    UpdateProductCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      await Validation(request.ToEntity());

      var response = await _repository.UpdateAsync(request.ToEntity());
      return ProductResponse.FromEntity(response!);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  private async Task Validation(Product product)
  {
    var response = await _repository.GetByIdAsync(product.Id)
      ?? throw new ProductNotFoundException();

    bool fieldsChanged = product.Name != response.Name || product.Color != response.Color;

    if (fieldsChanged && await _repository.ExistsAsync(product.Name, product.Color))
      throw new InvalidOperationException("Já existe um produto com esse nome e cor.");
  }
}
