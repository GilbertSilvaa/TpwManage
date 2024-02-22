using System.Xml.Linq;

namespace TpwManage.Core.Entities;

public class Selling : EntityBase
{
  public Guid ClientId { get; set; }
  public Client Client { get; set; } = default!;
  public List<Product> Products { get; private set; } = [];
  public float TotalPrice { get; private set; } = 0;

  public void SetupProducts(List<Product> products) 
  { 
    foreach (var product in products) 
    {
      TotalPrice += product.Price;
      Products.Add(product);
    }
  }

  public void ClearProducts()
  {
    TotalPrice = 0;
    Products.Clear();
  }

  public override string ToString()
  {
    return $@"
      Id: {Id};
      ClientId: {Client.Id}; 
      ClientName: {Client.Name}; 
      QtdeProducts: {Products.Count}
      TotalPrice: {TotalPrice}
      CreateAt: {CreateAt:mm:HH dd/MM/yyyy};
    ";
  }
}
