using TpwManage.Application.InputModels;
using TpwManage.Application.ViewModels;

namespace TpwManage.Application.Services.ClientService;

public interface IClientService
{
  Task<List<ClientViewModel>> GetAll();
  Task<ClientViewModel> GetById(Guid id);
  Task<ClientViewModel> Create(CreateClientInputModel model);
  Task<ClientViewModel> Update(UpdateClientInputModel model);
  Task<bool> Delete(Guid id);
}
