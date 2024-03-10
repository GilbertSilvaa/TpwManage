using MediatR;
using System.ComponentModel.DataAnnotations;
using TpwManage.Core.Entities;

namespace TpwManage.Application.ClientService.Commands.CreateClient;

public class CreateClientCommand : IRequest<CreateClientResponse>
{
  [Required(ErrorMessage = "Nome é um campo obrigatório.")]
  [StringLength(150, ErrorMessage = "O nome pode ter no maximo {1} caracteres.")]
  public string Name { get; set; } = string.Empty;

  public Client ToEntity() => new(Name);
}
