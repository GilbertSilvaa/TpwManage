using TpwManage.Application.InputModels;
using TpwManage.Application.ViewModels;

namespace TpwManage.Application.Services.ClientService;

public interface IClientService
{
  Task<List<ClientViewModel>> GetAll();
  Task<ClientViewModel> Create(CreateClientInputModel model);
}
