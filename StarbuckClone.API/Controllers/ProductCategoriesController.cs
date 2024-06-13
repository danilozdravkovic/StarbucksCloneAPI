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
        private UseCaseHandler _commandHandler;

        public ProductCategoriesController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }
        // GET: api/<ProductCategoriesController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductCategorySearchDto search, [FromServices] ISearchProductCategoriesQuery query)
        {
                var result = _commandHandler.HandleQuery(query, search);
                return Ok(result);
        }

        // GET api/<ProductCategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductCategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateProductCategoryDto dto, [FromServices] ICreateProductCategoryCommand command)
        {
                _commandHandler.HandleCommand(command, dto);
                return StatusCode(201);
    
        }

        // PUT api/<ProductCategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductCategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
