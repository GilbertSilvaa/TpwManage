using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TpwManage.Application.ProductService.Commands.CreateProduct;
using TpwManage.Application.ProductService.Commands.DeleteProduct;
using TpwManage.Application.ProductService.Commands.UpdateProduct;
using TpwManage.Application.ProductService.Queries.GetProductById;
using TpwManage.Application.ProductService.Queries.GetProducts;

namespace TpwManage.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IMediator mediator) : ControllerBase
{
  private readonly ISender _mediator = mediator;

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      GetProductsQuery query = new();
      return Ok(await _mediator.Send(query));
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpGet]
  [Route("{id}", Name = "GetProductById")]
  public async Task<IActionResult> Get(Guid id)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try 
    {
      GetProductByIdQuery query = new(id);
      var response = await _mediator.Send(query);
      return response is null ? NotFound() : Ok(response);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
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
  public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
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
      DeleteProductCommand command = new(id);
      var response = await _mediator.Send(command);
      return !response ? NotFound() : Ok("Produto deletado com sucesso.");
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }
}
