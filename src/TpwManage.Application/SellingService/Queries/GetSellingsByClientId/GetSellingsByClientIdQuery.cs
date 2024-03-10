using MediatR;

namespace TpwManage.Application.SellingService.Queries.GetSellingsByClientId;

public record GetSellingsByClientIdQuery(Guid ClientId) : IRequest<List<SellingResponse>>;

