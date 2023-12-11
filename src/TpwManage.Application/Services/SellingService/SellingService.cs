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

      List<Product> productList = [];

      foreach(var productId in model.ProductsId)
      {
        var product = await _productRepository.GetByIdAsync(productId);
        if(product != null) productList.Add(product);
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

  public async Task<SellingViewModel> Update(UpdateSellingInputModel model)
  {
    try
    {
      var client = await _clientRepository.GetByIdAsync(model.ClientId)
        ?? throw new KeyNotFoundException("Cliente não encontrado.");

      var productList = new List<Product>();
      foreach(var productId in model.ProductsId)
      {
        var product = await _productRepository.GetByIdAsync(productId);
        if(product != null) productList.Add(product);
      }

      Selling selling = new(){ Id = model.Id, Client = client };
      selling.SetupProducts(productList);

      var response = await _sellingRepository.UpdateAsync(selling)
        ?? throw new KeyNotFoundException("Venda não encontrada.");

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
}
