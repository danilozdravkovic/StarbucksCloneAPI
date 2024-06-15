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
        private UseCaseHandler _commandHandler;

        public CartLinesController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }
        // GET: api/<CartLinesController>
        [HttpGet]
        public IActionResult Get([FromBody] PagedSearchDto search, [FromServices] ISearchCartLinesQuery query)
        {
            var result = _commandHandler.HandleQuery(query, search);
            return Ok(result);
        }

        // GET api/<CartLinesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartLinesController>
        [HttpPost]
        public IActionResult Post([FromBody] AddCartLineDto dto, [FromServices] IAddCartLineCommand cmd)
        {
                _commandHandler.HandleCommand(cmd, dto);

                return StatusCode(201);
        }

        // PUT api/<CartLinesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartLinesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
