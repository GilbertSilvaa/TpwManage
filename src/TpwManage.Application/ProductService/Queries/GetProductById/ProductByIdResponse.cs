using TpwManage.Core.Entities;

namespace TpwManage.Application.ProductService.Queries.GetProductById;

internal class ProductByIdResponse(Guid id, string name, string color, float price, int amount)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;
  public string Color { get; set; } = color;
  public float Price { get; set; } = price;
  public int Amount { get; set; } = amount;

  public static ProductByIdResponse FromEntity(Product product)
    => new(
      product.Id,
      product.Name,
      product.Color,
      product.Price,
      product.Amount);
}
