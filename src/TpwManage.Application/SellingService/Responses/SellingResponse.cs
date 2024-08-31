using TpwManage.Core.Entities;

namespace TpwManage.Application;

internal class SellingResponse(
  Guid id,
  float totalPrice,
  DateTime dateSale,
  Client client,
  List<ProductSelling> products)
{
  public Guid Id { get; set; } = id;
  public Client Client { get; set; } = client;
  public List<ProductSelling> Products { get; set; } = products;
  public float TotalPrice { get; set; } = totalPrice;
  public DateTime DateSale { get; set; } = dateSale;

  public static SellingResponse FromEntity(Selling selling)
    => new(
      selling.Id,
      selling.TotalPrice,
      selling.CreateAt,
      selling.Client,
      [.. selling.Products.Select(ProductSelling.FromEntity)]);
}

internal class ProductSelling(Guid id, string name, string color, float price)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;
  public string Color { get; set; } = color;
  public float Price { get; set; } = price;

  public static ProductSelling FromEntity(Product product)
    => new(
      product.Id,
      product.Name,
      product.Color,
      product.Price);
}
