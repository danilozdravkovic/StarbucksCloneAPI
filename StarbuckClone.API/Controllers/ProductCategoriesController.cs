using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.ProductCategories;
using StarbucksClone.Application.UseCases.Queries.ProductCategories;
using StarbucksClone.Application.UseCases.Queries.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public ProductCategoriesController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }
        // GET: api/<ProductCategoriesController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductCategorySearchDto search, [FromServices] ISearchProductCategoriesQuery query)
        {
                var result = _useCaseHandler.HandleQuery(query, search);
                return Ok(result);
        }

        // GET api/<ProductCategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id , [FromServices] IGetProductCategoryQuery query)
        {
            var result = _useCaseHandler.HandleQuery(query, id);
            return Ok(result);
        }

        // POST api/<ProductCategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateProductCategoryDto dto, [FromServices] ICreateProductCategoryCommand command)
        {
                _useCaseHandler.HandleCommand(command, dto);
                return StatusCode(201);
    
        }

        // PUT api/<ProductCategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ModifyProductCategoryDto dto, [FromServices] IModifyProductCategoryCommand command)
        {
            dto.Id = id;
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // DELETE api/<ProductCategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProductCategoryCommand command)
        {
            _useCaseHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
