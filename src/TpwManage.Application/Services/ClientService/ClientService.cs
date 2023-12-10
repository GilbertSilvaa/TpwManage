using TpwManage.Application.InputModels;
using TpwManage.Application.ViewModels;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.Services.ClientService;

public class ClientService(IClientRepository repository) : IClientService
{
  private readonly IClientRepository _repository = repository;

  public async Task<List<ClientViewModel>> GetAll()
  {
    try 
    {
      var response = await _repository.GetAllAsync();
      return [.. response.Select(c => ClientViewModel.FromEntity(c)).OrderBy(c => c.Name)];
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<ClientViewModel> GetById(Guid id)
  {
    try 
    {
      var response = await _repository.GetByIdAsync(id) 
        ?? throw new KeyNotFoundException("Cliente não encontrado.");
        
      return ClientViewModel.FromEntity(response);
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<ClientViewModel> Create(CreateClientInputModel model)
  {
    try 
    {
      var client = model.ToEntity();     
      var clientExists = await _repository.ExistsAsync(client.Name);
      
      if(clientExists)    
        throw new InvalidOperationException("Já existe um cliente com esse nome.");

      var response = await _repository.CreateAsync(client);
      return ClientViewModel.FromEntity(response);
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<ClientViewModel> Update(UpdateClientInputModel model)
  {
    try 
    {
      var response = await _repository.UpdateAsync(model.ToEntity()) 
        ?? throw new KeyNotFoundException("Cliente não encontrado.");

      return ClientViewModel.FromEntity(response);
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<bool> Delete(Guid id)
  {
    try 
    {
      var response = await _repository.DeleteAsync(id);
      return response;
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
