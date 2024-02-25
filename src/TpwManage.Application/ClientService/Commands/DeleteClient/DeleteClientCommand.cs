using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TpwManage.Application.ClientService.Commands.DeleteClient;

public class DeleteClientCommand : IRequest<bool>
{
  [Required(ErrorMessage = "Id é um campo obrigatório.")]
  public Guid Id { get; set; }
}

