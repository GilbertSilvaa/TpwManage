using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ProductService.Commands.CreateProduct;

internal class CreateProductCommandHandler(IProductRepository repository)
  : IRequestHandler<CreateProductCommand, CreateProductResponseDto>
{
  private readonly IProductRepository _repository = repository;

  public async Task<CreateProductResponseDto> Handle(
    CreateProductCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var product = request.ToEntity();
      var productExists = await _repository.ExistsAsync(product.Name, product.Color);

      if (productExists)
        throw new InvalidOperationException("Esse produto já existe.");

      var response = await _repository.CreateAsync(product);
      return CreateProductResponseDto.FromEntity(response);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
