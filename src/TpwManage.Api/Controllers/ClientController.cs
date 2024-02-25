using System.Net;
using Microsoft.AspNetCore.Mvc;
using TpwManage.Application.InputModels;
using TpwManage.Application.Services.ClientService;

namespace TpwManage.Api.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController(IClientService service) : ControllerBase
{
  private readonly IClientService _service = service;

  [HttpGet]
  public async Task<IActionResult> GetAll() 
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      return Ok(await _service.GetAll());
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
      var response = await _service.GetById(id);      
      return response == null ? NotFound() : Ok(response);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateClientInputModel model)
  {
    if (!ModelState.IsValid || model == null) return BadRequest(ModelState);
    
    try 
    {
      var response = await _service.Create(model);
      var linkRedirect = Url.Link("GetClientById", new { id =  response.Id})!;
      return Created(new Uri(linkRedirect), response);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }  
  }

  [HttpPut]
  public async Task<IActionResult> Update([FromBody] UpdateClientInputModel model)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      var response = await _service.Update(model);  
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
      var response = await _service.Delete(id);
      return !response ? NotFound() : Ok("Cliente deletado com sucesso.");
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }
}
