using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.SellingService.Commands.DeleteSelling;

internal class DeleteSellingCommandHandler(ISellingRepository repository)
  : IRequestHandler<DeleteSellingCommand, bool>
{
  private readonly ISellingRepository _repository = repository;

  public async Task<bool> Handle(
    DeleteSellingCommand request, 
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
