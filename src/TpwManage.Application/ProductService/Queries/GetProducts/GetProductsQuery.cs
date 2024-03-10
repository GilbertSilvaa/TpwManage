using MediatR;

namespace TpwManage.Application.ProductService.Queries.GetProducts;

public record GetProductsQuery : IRequest<List<CreateProductResponse>>;
