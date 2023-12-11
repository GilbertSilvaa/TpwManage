using TpwManage.Core.Entities;

namespace TpwManage.Application.ViewModels;

public class SellingViewModel(
  Guid id, 
  Client client, 
  List<Product>products, 
  float totalPrice, 
  DateTime dateSale
)
{
  public Guid Id { get; set; } = id;
  public Client Client { get; set; } = client;
  public List<Product> Products { get; set; } = products;
  public float TotalPrice { get; set; } = totalPrice;
  public DateTime DateSale { get; set; } = dateSale;

  public static SellingViewModel FromEntity(Selling selling)
    => new(
      selling.Id, 
      selling.Client, 
      selling.Products, 
      selling.TotalPrice, 
      selling.CreateAt
    );
}
