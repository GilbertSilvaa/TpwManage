namespace TpwManage.Core.Entities;

public class Product : EntityBase
{
  public Product() 
  { 
    Name = string.Empty;
    Color = string.Empty;
    Price = float.MinValue;
    Amount = int.MinValue;
  }

  public Product(string name, string color, float price, int amount)
  {
    Name = name;
    Color = color;
    Price = price;
    Amount = amount;
  }

  public string Name { get; set; }
  public string Color { get; set; }
  public float Price { get; set; }
  public int Amount { get; set; }
}
