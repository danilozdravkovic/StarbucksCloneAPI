using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.CartLines;
using StarbucksClone.Application.UseCases.Commands.Orders;
using StarbucksClone.Application.UseCases.Queries.Orders;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public OrdersController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }
        // GET: api/<OrdersController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] SearchOrderDto search, [FromServices] ISearchOrdersQuery query )
        {
            var result = _useCaseHandler.HandleQuery(query, search);

            return Ok(result);
        }



        // POST api/<OrdersController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto dto, [FromServices] ICreateOrderCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);

            return StatusCode(201);
        }


        // DELETE api/<OrdersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteOrderCommand command)
        {
            
            _useCaseHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
