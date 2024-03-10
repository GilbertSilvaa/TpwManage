using MediatR;

namespace TpwManage.Application.ClientService.Queries.GetClients;

public record GetClientsQuery : IRequest<List<ClientResponse>>;
