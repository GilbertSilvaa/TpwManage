using TpwManage.Core.Entities;

namespace TpwManage.Application.InputModels;

public class CreateClientInputModel
{
  public string Name { get; set; } = string.Empty;
  
  public Client ToEntity() => new(Name);
}

public class UpdateClientInputModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;

  public Client ToEntity() => new(Name, Id);
}

