using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class SellingCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;

  [Fact]
  public async Task IsPossibleCrudSelling()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);
    ProductRepository _productRepository = new(context!);
    ClientRepository _clientRepository = new(context!);
    Selling selling = new() 
    { 
      Client = await GenerateClient(_clientRepository)
    };

    selling.SetupProducts(await GenerateProductList(_productRepository));

    // create selling
    Selling sellingCreated = await _sellingRepository.CreateAsync(selling);
    Assert.NotNull(sellingCreated);
    Assert.Equal(sellingCreated.Client, selling.Client);
    Assert.Equal(sellingCreated.Products, selling.Products);
    Assert.Equal(sellingCreated.TotalPrice, selling.TotalPrice);
    Assert.True(sellingCreated.Products.Count == selling.Products.Count);
    Assert.False(sellingCreated.Products.Count == 0);
    Assert.NotEqual(sellingCreated.Id, Guid.Empty);

    // update selling
    Selling sellingUpdate = new()
    {
      Id = sellingCreated.Id,
      Client = await GenerateClient(_clientRepository),
      CreateAt = sellingCreated.CreateAt  
    };
    sellingUpdate.SetupProducts(await GenerateProductList(_productRepository));

    var sellingUpdated = await _sellingRepository.UpdateAsync(sellingUpdate);
    Assert.NotNull(sellingUpdated);
    Assert.Equal(sellingUpdated.Id, sellingUpdate.Id);
    Assert.Equal(sellingUpdated.Client, sellingUpdate.Client);
    Assert.Equal(sellingUpdated.Products, sellingUpdate.Products);
    Assert.Equal(sellingUpdated.TotalPrice, sellingUpdate.TotalPrice);
    Assert.Equal(sellingUpdated.CreateAt, sellingUpdate.CreateAt);
    Assert.True(sellingUpdated.Products.Count == sellingUpdate.Products.Count);
    Assert.False(sellingUpdated.Products.Count == 0);

    // select selling by Id
    var sellingSelected = await _sellingRepository.GetByIdAsync(sellingUpdated.Id);
    Assert.NotNull(sellingSelected);
    Assert.Equal(sellingSelected.Id, sellingUpdated.Id);
    Assert.Equal(sellingSelected.Client, sellingUpdated.Client);
    Assert.Equal(sellingSelected.Products, sellingUpdated.Products);
    Assert.Equal(sellingSelected.TotalPrice, sellingUpdated.TotalPrice);
    Assert.True(sellingSelected.Products.Count == sellingUpdated.Products.Count);
    Assert.False(sellingSelected.Products.Count == 0);

    // select all sellings
    var allSellingsList = await _sellingRepository.GetAllAsync();
    Assert.NotNull(allSellingsList);
    Assert.True(allSellingsList.Count > 0);

    // delete selling
    bool isDeleted = await _sellingRepository.DeleteAsync(sellingUpdated.Id);
    Assert.True(isDeleted);

    // select empty selling list
    allSellingsList = await _sellingRepository.GetAllAsync();
    Assert.NotNull(allSellingsList);
    Assert.Empty(allSellingsList);
  }

  private static async Task<List<Product>> GenerateProductList(ProductRepository repository)
  {
    List<Product> products = [];
    Random random = new();

    foreach (var _ in Enumerable.Range(1, 9))
    {
      Product product = new(
        name: Faker.Name.First(),
        color: Faker.Name.Last(),
        price: random.Next(20, 80),
        amount: random.Next(5, 35));

      var productCreated = await repository.CreateAsync(product);
      products.Add(productCreated);
    }

    return products;
  }

  private static async Task<Client> GenerateClient(ClientRepository repository)
    => await repository.CreateAsync(new(Faker.Name.FullName()));
}
