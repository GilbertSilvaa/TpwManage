using TpwManage.Application.InputModels;
using TpwManage.Application.ViewModels;

namespace TpwManage.Application.Services.ProductService;

public interface IProductService
{
  Task<List<ProductViewModel>> GetAll();
  Task<ProductViewModel> GetById(Guid id);
  Task<ProductViewModel> Create(CreateProductInputModel model);
  Task<ProductViewModel> Update(UpdateProductInputModel model);
  Task<bool> Delete(Guid id);
}
