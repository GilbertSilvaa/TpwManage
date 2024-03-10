using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ProductService.Queries.GetProducts;

internal class GetProductsQueryHandler(IProductRepository repository) 
  : IRequestHandler<GetProductsQuery, List<CreateProductResponse>>
{
  private readonly IProductRepository _repository = repository;

  public async Task<List<CreateProductResponse>> Handle(
    GetProductsQuery request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var response = await _repository.GetAllAsync();
      return [.. response.Select(CreateProductResponse.FromEntity).OrderBy(p => p.Name)];
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
