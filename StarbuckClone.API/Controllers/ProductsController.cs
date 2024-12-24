using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Command.Products;
using StarbucksClone.Application.UseCases.Queries.Products;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private UseCaseHandler _useCaseHandler;

        public ProductsController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearchDto search, [FromServices] ISearchProductsQuery query)
        {
            var result = _useCaseHandler.HandleQuery(query, search);
            return Ok(result);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery] IDProductDto search, [FromServices] IGetProductQuery query)
        {
            search.Id = id;
            var result=_useCaseHandler.HandleQuery(query, search);
            return Ok(result);
        }

        // POST api/<ProductsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateProductDto dto,[FromServices] ICreateProductCommand command)
        {
                _useCaseHandler.HandleCommand(command, dto);
                return StatusCode(201);

        }

        // PUT api/<ProductsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ModifyProductDto dto, [FromServices] IModifyProductCommand command)
        {
            dto.Id = id;
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // DELETE api/<ProductsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProductCommand command)
        {
            
            _useCaseHandler.HandleCommand(command, id);

            return NoContent();
        }
    }
}
