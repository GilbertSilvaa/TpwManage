using MediatR;

namespace TpwManage.Application.ProductService.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;

