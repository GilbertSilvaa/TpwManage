using TpwManage.Core.Entities;

namespace TpwManage.Application.ViewModels;

public class SellingViewModel(
  Guid id, 
  float totalPrice, 
  DateTime dateSale,
  ClientViewModel client, 
  List<ProductSellingViewModel>products
)
{
  public Guid Id { get; set; } = id;
  public ClientViewModel Client { get; set; } = client;
  public List<ProductSellingViewModel> Products { get; set; } = products;
  public float TotalPrice { get; set; } = totalPrice;
  public DateTime DateSale { get; set; } = dateSale;

  public static SellingViewModel FromEntity(Selling selling)
    => new(
      selling.Id, 
      selling.TotalPrice, 
      selling.CreateAt,
      ClientViewModel.FromEntity(selling.Client), 
      [.. selling.Products.Select(p => ProductSellingViewModel.FromEntity(p))]
    );
}

public class ProductSellingViewModel(Guid id, string name, string color, float price)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;
  public string Color { get; set; } = color;
  public float Price { get; set; } = price;

  public static ProductSellingViewModel FromEntity(Product product) 
    => new(
      product.Id, 
      product.Name, 
      product.Color, 
      product.Price
    );
}
