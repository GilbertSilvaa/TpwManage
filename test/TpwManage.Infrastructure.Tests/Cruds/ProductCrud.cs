using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class ProductCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;

  [Fact]
  public async Task IsPossibleCrudProduct()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    Product product = new(
      name: Faker.Name.First(), 
      color: Faker.Name.Last(), 
      price: Faker.RandomNumber.Next(), 
      amount: Faker.RandomNumber.Next());

    // create product
    Product productCreated = await _repository.CreateAsync(product);
    Assert.NotNull(productCreated);
    Assert.Equal(product.Id, productCreated.Id);
    Assert.Equal(product.Name, productCreated.Name);
    Assert.False(productCreated.Id == Guid.Empty);

    // update product
    product.Name = Faker.Name.First();
    product.Color = Faker.Name.Last();
    product.Price = Faker.RandomNumber.Next();
    product.Amount = Faker.RandomNumber.Next();

    var productUpdated = await _repository.UpdateAsync(product);
    Assert.NotNull(productUpdated);
    Assert.Equal(product.Name, productUpdated.Name);
    Assert.Equal(product.Color, productUpdated.Color);
    Assert.Equal(product.Price, productUpdated.Price);
    Assert.Equal(product.Amount, productUpdated.Amount);

    // verify if product exist
    bool productExist = await _repository.ExistsAsync(productUpdated.Name, productUpdated.Color);
    Assert.True(productExist);

    productExist = await _repository.ExistsAsync(Faker.Name.First(), Faker.Name.Last());
    Assert.False(productExist);

    // select product by Id
    var productSelected = await _repository.GetByIdAsync(productUpdated.Id);
    Assert.NotNull(productSelected);
    Assert.Equal(productUpdated.Name, productSelected.Name);
    Assert.Equal(productUpdated.Color, productSelected.Color);
    Assert.Equal(productUpdated.Price, productSelected.Price);
    Assert.Equal(productUpdated.Amount, productSelected.Amount);
    Assert.Equal(productUpdated.Id, productSelected.Id);

    // select all products
    var allProductsList = await _repository.GetAllAsync();
    Assert.NotNull(allProductsList);
    Assert.True(allProductsList.Count > 0);

    // delete product
    bool isDeleted = await _repository.DeleteAsync(productSelected.Id);
    Assert.True(isDeleted);

    // select empty product list
    allProductsList = await _repository.GetAllAsync();
    Assert.NotNull(allProductsList);
    Assert.True(allProductsList.Count == 0);
  }
}
