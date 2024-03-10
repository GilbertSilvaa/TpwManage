using MediatR;
using TpwManage.Core;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ProductService.Queries.GetProductById;

internal class GetProductByIdQueryHandler(IProductRepository repository)
  : IRequestHandler<GetProductByIdQuery, ProductByIdResponse>
{
  private readonly IProductRepository _repository = repository;

  public async Task<ProductByIdResponse> Handle(
    GetProductByIdQuery request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var response = await _repository.GetByIdAsync(request.Id)
        ?? throw new ProductNotFoundException();

      return ProductByIdResponse.FromEntity(response);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
