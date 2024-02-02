using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests;

public class CreateClient(DbTest dbTest) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = dbTest.ServiceProvider;

  [Fact]
  public async Task IsPossibleToCreateClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository repository = new(context!);

    Client client = new(Faker.Name.FullName());
    Client clientCreated = await repository.CreateAsync(client);

    Assert.NotNull(clientCreated);
    Assert.Equal(client.Name, clientCreated.Name);
    Assert.False(clientCreated.Id == Guid.Empty);
  }
}
