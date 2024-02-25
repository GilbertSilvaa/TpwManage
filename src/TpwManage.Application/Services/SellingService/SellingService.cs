using TpwManage.Application.InputModels;
using TpwManage.Application.ViewModels;
using TpwManage.Core;
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
      return [.. response.Select(SellingViewModel.FromEntity)
        .OrderByDescending(s => s.DateSale)];
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<SellingViewModel> GetById(Guid id)
  {
    try 
    {
      var response = await _sellingRepository.GetByIdAsync(id) 
        ?? throw new SellingNotFoundException();  

      return SellingViewModel.FromEntity(response);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
  
  public async Task<List<SellingViewModel>> GetByClientId(Guid clientId)
  {
    try
    {
      var response = await _sellingRepository.GetByClientIdAsync(clientId);
      return [.. response.Select(SellingViewModel.FromEntity)
        .OrderByDescending(s => s.DateSale)];
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<SellingViewModel> Create(CreateSellingInputModel model)
  {
    try
    {
      var client = await _clientRepository.GetByIdAsync(model.ClientId)
        ?? throw new ClientNotFoundException();

      List<Product> productList = [];
      foreach (var productId in model.ProductsId)
      {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) continue;
        
        var stockExists = await ChangeAmountProductStock(product, -1);
        if (stockExists) productList.Add(product);
      }

      Selling selling = new(client);
      selling.SetupProducts(productList);

      var response = await _sellingRepository.CreateAsync(selling);
      return SellingViewModel.FromEntity(response);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<SellingViewModel> Update(UpdateSellingInputModel model)
  {
    try
    {
      var selling = await _sellingRepository.GetByIdAsync(model.Id)
        ?? throw new SellingNotFoundException();
      
      foreach (var product in selling.Products)
        await ChangeAmountProductStock(product, 1);   
      
      List<Product> productList = [];
      foreach (var productId in model.ProductsId)
      {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) continue;    

        var stockExists = await ChangeAmountProductStock(product, -1);
        if (stockExists) productList.Add(product);
      }

      Selling sellingUpdate = new(selling.Client)
      { 
        Id = selling.Id, 
        CreateAt = selling.CreateAt
      };
      
      sellingUpdate.SetupProducts(productList);

      var response = await _sellingRepository.UpdateAsync(sellingUpdate);
      return SellingViewModel.FromEntity(response!);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<bool> Delete(Guid id)
  {
    try 
    {
      return await _sellingRepository.DeleteAsync(id);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  private async Task<bool> ChangeAmountProductStock(Product product, int amount)
  {
    try 
    {
      if (product.Amount == 0) return false;
      
      product.Amount += amount;
      var response = await _productRepository.UpdateAsync(product);      
      return response is not null;
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
