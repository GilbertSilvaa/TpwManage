using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TpwManage.Application.SellingService.Commands.CreateSelling;
using TpwManage.Application.SellingService.Commands.DeleteSelling;
using TpwManage.Application.SellingService.Commands.UpdateSelling;
using TpwManage.Application.SellingService.Queries.GetSellingById;
using TpwManage.Application.SellingService.Queries.GetSellings;
using TpwManage.Application.SellingService.Queries.GetSellingsByClientId;

namespace TpwManage.Api.Controllers;

[ApiController]
[Route("api/selling")]
public class SellingController(IMediator mediator) : ControllerBase
{
  private readonly ISender _mediator = mediator;

  [HttpGet]
  public async Task<IActionResult> GetAll() 
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    
    try 
    {
      GetSellingsQuery query = new();
      return Ok(await _mediator.Send(query));
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpGet]
  [Route("{id}", Name = "GetSellingById")]
  public async Task<IActionResult> GetById(Guid id)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      GetSellingByIdQuery query = new(id);
      var response = await _mediator.Send(query);
      return response == null ? NotFound() : Ok(response);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpGet]
  [Route("client/{clientId}", Name = "GetSellingByClientId")]
  public async Task<IActionResult> GetByClientId(Guid clientId)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try
    {
      GetSellingsByClientIdQuery query = new(clientId);
      return Ok(await _mediator.Send(query));
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateSellingCommand command)
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
  public async Task<IActionResult> Update([FromBody] UpdateSellingCommand command)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      var response = await _mediator.Send(command);
      return response == null ? NotFound() : Ok(response);
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
      DeleteSellingCommand command = new(id);
      var response = await _mediator.Send(command); 
      return !response ? NotFound() : Ok("Venda deletada com sucesso.");
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }
}
