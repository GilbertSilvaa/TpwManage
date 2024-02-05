using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class ClientCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;

  [Fact]
  public async Task IsPossibleCrudClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    Client client = new(Faker.Name.FullName());

    // create client
    Client clientCreated = await _repository.CreateAsync(client);
    Assert.NotNull(clientCreated);
    Assert.Equal(client.Id, clientCreated.Id);
    Assert.Equal(client.Name, clientCreated.Name);
    Assert.False(clientCreated.Id == Guid.Empty);

    // update client
    client.Name = Faker.Name.FullName();
    var clientUpdated = await _repository.UpdateAsync(client);
    Assert.NotNull(clientUpdated);
    Assert.Equal(client.Name, clientUpdated.Name);
    Assert.True(client.Id == clientUpdated.Id);
    Assert.False(clientUpdated.Id == Guid.Empty);

    // verify if client exist
    bool clientExist = await _repository.ExistsAsync(clientUpdated.Name);
    Assert.True(clientExist);
    clientExist= await _repository.ExistsAsync(Faker.Name.FullName());
    Assert.False(clientExist);

    // select client by Id
    var clientSelected = await _repository.GetByIdAsync(clientUpdated.Id);
    Assert.NotNull(clientSelected);
    Assert.Equal(clientUpdated.Name, clientSelected.Name);
    Assert.True(clientUpdated.Id == clientSelected.Id);
    Assert.False(clientSelected.Id == Guid.Empty);

    // select all clients
    var allClientsList = await _repository.GetAllAsync();
    Assert.NotNull(allClientsList);
    Assert.True(allClientsList.Count > 0);

    // delete client
    bool isDeleted = await _repository.DeleteAsync(clientSelected.Id);
    Assert.True(isDeleted);

    // select empty client list
    allClientsList = await _repository.GetAllAsync();
    Assert.NotNull(allClientsList);
    Assert.True(allClientsList.Count == 0);
  }
}