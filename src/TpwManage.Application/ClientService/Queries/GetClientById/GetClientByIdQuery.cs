using MediatR;

namespace TpwManage.Application.ClientService.Queries.GetClientById;

public record GetClientByIdQuery(Guid Id) : IRequest<ClientResponse>;


