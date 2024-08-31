using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TpwManage.Application.ClientService.Commands.CreateClient;
using TpwManage.Application.ClientService.Commands.DeleteClient;
using TpwManage.Application.ClientService.Commands.UpdateClient;
using TpwManage.Application.ClientService.Queries.GetClientById;
using TpwManage.Application.ClientService.Queries.GetClients;

namespace TpwManage.Api.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController(IMediator mediator) : ControllerBase
{
  private readonly ISender _mediator = mediator;

  [HttpGet]
  public async Task<IActionResult> GetAll() 
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      GetClientsQuery query = new();
      return Ok(await _mediator.Send(query));
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpGet]
  [Route("{id}", Name = "GetClientById")]
  public async Task<IActionResult> Get(Guid id)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      GetClientByIdQuery query = new(id);
      var response = await _mediator.Send(query);
      return response is null ? NotFound() : Ok(response);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateClientCommand command)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    
    try 
    {
      var response = await _mediator.Send(command);
      return StatusCode((int)HttpStatusCode.Created, response);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }  
  }

  [HttpPut]
  public async Task<IActionResult> Update([FromBody] UpdateClientCommand command)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      var response = await _mediator.Send(command);
      return response is null ? NotFound() : Ok(response);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpDelete]
  [Route("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      DeleteClientCommand command = new(id);
      var response = await _mediator.Send(command);
      return !response ? NotFound() : Ok("Cliente deletado com sucesso.");
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }
}
