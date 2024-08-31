using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ProductService.Commands.DeleteProduct;

internal class DeleteProductCommandHandler(IProductRepository repository) 
  : IRequestHandler<DeleteProductCommand, bool>
{
  private readonly IProductRepository _repository = repository;

  public async Task<bool> Handle(
    DeleteProductCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      return await _repository.DeleteAsync(request.Id);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
