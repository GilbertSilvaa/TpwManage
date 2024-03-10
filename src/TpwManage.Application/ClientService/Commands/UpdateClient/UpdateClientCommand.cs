using MediatR;
using System.ComponentModel.DataAnnotations;
using TpwManage.Core.Entities;

namespace TpwManage.Application.ClientService.Commands.UpdateClient;

public class UpdateClientCommand : IRequest<ClientResponse>
{
  [Required(ErrorMessage = "Id é um campo obrigatório.")]
  public Guid Id { get; set; }

  [Required(ErrorMessage = "Nome é um campo obrigatório.")]
  [StringLength(150, ErrorMessage = "O nome pode ter no maximo {1} caracteres.")]
  public string Name { get; set; } = string.Empty;

  public Client ToEntity() => new(Name) { Id = Id };
}
