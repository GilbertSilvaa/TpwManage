using MediatR;

namespace TpwManage.Application.SellingService.Queries.GetSellingById;

public record GetSellingByIdQuery(Guid Id) : IRequest<SellingResponse>;

