using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.CartLines;
using StarbucksClone.Application.UseCases.Commands.Users;
using StarbucksClone.Application.UseCases.Queries.CartLines;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartLinesController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public CartLinesController(UseCaseHandler useCaseHanlder)
        {
            _useCaseHandler = useCaseHanlder;
        }
        // GET: api/<CartLinesController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearchDto search, [FromServices] ISearchCartLinesQuery query)
        {
            var result = _useCaseHandler.HandleQuery(query, search);
            return Ok(result);
        }

        // GET api/CartLinesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, IGetProductFromCartQuery query)
        {
            var result = _useCaseHandler.HandleQuery(query, id);
            return Ok(result);
        }

        // POST api/<CartLinesController>
        [HttpPost]
        public IActionResult Post([FromBody] AddCartLineDto dto, [FromServices] IAddCartLineCommand command)
        {
                _useCaseHandler.HandleCommand(command, dto);

                return StatusCode(201);
        }

        // PUT api/<CartLinesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ModifyCartLineDto dto, [FromServices] IModifyCartLineCommand command)
        {
            dto.CartLineId = id;
            _useCaseHandler.HandleCommand(command, dto);

            return StatusCode(201);
        }

        // DELETE api/<CartLinesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCartLineCommand command)
        {
            _useCaseHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
