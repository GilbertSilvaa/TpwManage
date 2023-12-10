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
    try 
    {
      var response = await _service.GetAll();
      return Ok(response);
    }
    catch(Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateClientInputModel model)
  {
    if (!ModelState.IsValid || model == null) return BadRequest(ModelState);
    
    try 
    {
      await _service.Create(model);
      return Created();
    }
    catch(Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }  
  }
}
