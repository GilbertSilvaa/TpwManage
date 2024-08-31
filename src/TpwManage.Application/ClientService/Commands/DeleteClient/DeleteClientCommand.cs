using MediatR;

namespace TpwManage.Application.ClientService.Commands.DeleteClient;

public record DeleteClientCommand(Guid Id) : IRequest<bool>;
