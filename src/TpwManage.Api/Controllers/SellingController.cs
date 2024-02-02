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
      var response = await _service.GetById(id);
      return response == null ? NotFound() : Ok(response);
    }
    catch (Exception ex)
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
      var linkRedirect = Url.Link("GetSellingById", new { id = response.Id })!;
      return Created(new Uri(linkRedirect), response);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }  
  }
  
  [HttpPut]
  public async Task<IActionResult> Update([FromBody] UpdateSellingInputModel model)
  {
    if (!ModelState.IsValid || model == null) return BadRequest(ModelState);

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
      return !response ? NotFound() : Ok("Venda deletada com sucesso.");
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }
}
