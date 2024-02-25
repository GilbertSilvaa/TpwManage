using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public sealed class SellingCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;
  private List<Product>? _productList;
  private Selling? _selling;

  [Fact]
  public async Task IsPossible_CreateSelling()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    Selling selling = new(await CreateClientAsync(new(context!)));
    _productList ??= await GenerateProductListAsync(new(context!));
    selling.SetupProducts(_productList.Take(4).ToList());
    Selling sellingCreated = await _sellingRepository.CreateAsync(selling);
    _selling = sellingCreated;

    Assert.Equal(selling.ToString(), sellingCreated.ToString());
  }
  
  [Fact]
  public async Task IsPossible_SelectAllSellings()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    _selling ??= await CreateSellingAsync(context!);
    var allSellingsList = await _sellingRepository.GetAllAsync();

    Assert.True(allSellingsList.Count > 0);
  }

  [Fact]
  public async Task IsPossible_SelectSellingById()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    _selling ??= await CreateSellingAsync(context!);
    var sellingSelected = await _sellingRepository.GetByIdAsync(_selling.Id);

    Assert.Equal(_selling.ToString(), sellingSelected?.ToString());
  }

  [Fact]
  public async Task IsPossible_UpdateSelling()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    _selling ??= await CreateSellingAsync(context!);
    _selling.Client = await CreateClientAsync(new(context!));
    _selling.ClearProducts();
    _productList ??= await GenerateProductListAsync(new(context!));
    _selling.SetupProducts(_productList.Take(6).ToList());
    var sellingUpdated = await _sellingRepository.UpdateAsync(_selling);

    Assert.Equal(_selling.ToString(), sellingUpdated?.ToString());
  }

  [Fact]
  public async Task IsPossible_DeleteSelling()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    _selling ??= await CreateSellingAsync(context!);
    var isDeleted = await _sellingRepository.DeleteAsync(_selling.Id);

    Assert.True(isDeleted);
  }
  
  private async Task<Selling> CreateSellingAsync(MyContext context)
  {
    SellingRepository repository = new(context!);

    Selling selling = new(await CreateClientAsync(new(context!)));
    _productList ??= await GenerateProductListAsync(new(context!));
    selling.SetupProducts(_productList.Take(4).ToList());

    return await repository.CreateAsync(selling);
  }

  private static async Task<List<Product>> GenerateProductListAsync(ProductRepository repository)
  {
    List<Product> products = [];
    Random random = new();

    foreach (var _ in Enumerable.Range(0, 8))
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

  private static async Task<Client> CreateClientAsync(ClientRepository repository)
    => await repository.CreateAsync(new(Faker.Name.FullName()));
}
