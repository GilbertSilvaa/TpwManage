using TpwManage.Application.InputModels;
using TpwManage.Application.ViewModels;

namespace TpwManage.Application.Services.SellingService;

public interface ISellingService
{
  Task<List<SellingViewModel>> GetAll();
  Task<SellingViewModel> GetById(Guid id);
  Task<List<SellingViewModel>> GetByClientId(Guid clientId);
  Task<SellingViewModel> Create(CreateSellingInputModel model);
  Task<SellingViewModel> Update(UpdateSellingInputModel model);
  Task<bool> Delete(Guid id);
}
