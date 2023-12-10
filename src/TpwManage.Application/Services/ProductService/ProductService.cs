using TpwManage.Application.InputModels;
using TpwManage.Application.ViewModels;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.Services.ProductService;

public class ProductService(IProductRepository repository) : IProductService
{
  private readonly IProductRepository _repository = repository;

  public async Task<List<ProductViewModel>> GetAll()
  {
    try 
    {
      var response = await _repository.GetAllAsync();
      return [.. response.Select(p => ProductViewModel.FromEntity(p)).OrderBy(p => p.Name)];
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<ProductViewModel> GetById(Guid id)
  {
    try 
    {
      var response = await _repository.GetByIdAsync(id) 
        ?? throw new KeyNotFoundException("Produto não encontrado.");
        
      return ProductViewModel.FromEntity(response);
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<ProductViewModel> Create(CreateProductInputModel model)
  {
    try 
    {
      var product = model.ToEntity();     
      var productExists = await _repository.ExistsAsync(product.Name);
      
      if(productExists)    
        throw new InvalidOperationException("Já existe um produto com esse nome.");

      var response = await _repository.CreateAsync(product);
      return ProductViewModel.FromEntity(response);
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<ProductViewModel> Update(UpdateProductInputModel model)
  {
    try 
    {
      var response = await _repository.UpdateAsync(model.ToEntity()) 
        ?? throw new KeyNotFoundException("Produto não encontrado.");

      return ProductViewModel.FromEntity(response);
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
