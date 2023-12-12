using TpwManage.Application.InputModels;
using TpwManage.Application.ViewModels;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.Services.SellingService;

public class SellingService(
  ISellingRepository repository, 
  IClientRepository clientRepository, 
  IProductRepository productRepository
) : ISellingService
{
  private readonly ISellingRepository _sellingRepository = repository;
  private readonly IClientRepository _clientRepository = clientRepository;
  private readonly IProductRepository _productRepository = productRepository;

  public async Task<List<SellingViewModel>> GetAll()
  {
    try 
    {
      var response = await _sellingRepository.GetAllAsync();
      return [.. response.Select(s => SellingViewModel.FromEntity(s))
        .OrderByDescending(s => s.DateSale)];
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<SellingViewModel> GetById(Guid id)
  {
    try 
    {
      var response = await _sellingRepository.GetByIdAsync(id) 
        ?? throw new KeyNotFoundException("Venda não encontrada.");
        
      return SellingViewModel.FromEntity(response);
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
  public async Task<SellingViewModel> Create(CreateSellingInputModel model)
  {
    try
    {
      var client = await _clientRepository.GetByIdAsync(model.ClientId)
        ?? throw new KeyNotFoundException("Cliente não encontrado.");

      var productList = new List<Product>();
      foreach(var productId in model.ProductsId)
      {
        var product = await _productRepository.GetByIdAsync(productId);
        if(product != null) 
        {
          var stockExists = await RemoveProductStock(product, 1);
          if(stockExists) productList.Add(product);
        }
      }

      Selling selling = new(){ Client = client };
      selling.SetupProducts(productList);

      var response = await _sellingRepository.CreateAsync(selling);
      return SellingViewModel.FromEntity(response);
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
      var response = await _sellingRepository.DeleteAsync(id);
      return response;
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public Task<SellingViewModel> Update(UpdateSellingInputModel model)
  {
    throw new NotImplementedException();
  }

  private async Task<bool> RemoveProductStock(Product product, int amount)
  {
    try 
    {
      if(product.Amount == 0) return false;

      product.Amount -= amount; 
      var response = await _productRepository.UpdateAsync(product);
      
      return response != null;
    }
    catch(Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
