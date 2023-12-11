using System.Net;
using Microsoft.AspNetCore.Mvc;
using TpwManage.Application.InputModels;
using TpwManage.Application.Services.SellingService;

namespace TpwManage.Api.Controllers;

[ApiController]
[Route("api/selling")]
public class SellingController(ISellingService service) : ControllerBase
{
  private readonly ISellingService _service = service;

  [HttpGet]
  public async Task<IActionResult> GetAll() 
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

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
  public async Task<IActionResult> Create([FromBody] CreateSellingInputModel model)
  {
    if (!ModelState.IsValid || model == null) return BadRequest(ModelState);
    
    try 
    {
      var response = await _service.Create(model);
      return Created();
    }
    catch(Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }  
  }
}
