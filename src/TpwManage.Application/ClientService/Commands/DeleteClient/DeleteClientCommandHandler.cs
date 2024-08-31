using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ClientService.Commands.DeleteClient;

internal class DeleteClientCommandHandler(IClientRepository repository)
  : IRequestHandler<DeleteClientCommand, bool>
{
  private readonly IClientRepository _repository = repository;

  public async Task<bool> Handle(
    DeleteClientCommand request, 
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
