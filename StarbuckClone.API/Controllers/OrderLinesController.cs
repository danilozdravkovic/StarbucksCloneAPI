using Microsoft.AspNetCore.Mvc;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.CartLines;
using StarbucksClone.Application.UseCases.Queries.OrderLines;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLinesController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public OrderLinesController(UseCaseHandler useCaseHanlder)
        {
            _useCaseHandler = useCaseHanlder;
        }
        // GET: api/<OrderLinesController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearchDto search, [FromServices] IGetFavouriteProductsQuery query)
        {
            var result = _useCaseHandler.HandleQuery(query, search);
            return Ok(result);
        }

        // GET api/<OrderLinesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderLinesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderLinesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderLinesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
