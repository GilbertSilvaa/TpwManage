namespace TpwManage.Core.Entities;

public class Selling(Guid? id = null) : EntityBase(id)
{
  public Client Client { get; set; } = default!;
  public List<Product> Products { get; private set; } = [];
  public float TotalPrice { get; private set; } = 0;

  public void SetupProducts(List<Product> products) 
  { 
    foreach (var product in products) {
      TotalPrice += product.Price;
      Products.Add(product);
    }
  }
}
