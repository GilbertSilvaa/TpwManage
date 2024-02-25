using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TpwManage.Application.ClientService.Queries.GetClientById;

public class GetClientByIdQuery : IRequest<ClientByIdResponseDto>
{
  [Required(ErrorMessage = "Id é um campo obrigatório.")]
  public Guid Id { get; set; }
}

