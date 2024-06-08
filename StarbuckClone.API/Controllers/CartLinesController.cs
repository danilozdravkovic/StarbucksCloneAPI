using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.CartLines;
using StarbucksClone.Application.UseCases.Commands.Users;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
            try
            {
                _commandHandler.HandleCommand(cmd, dto);

                return StatusCode(201);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(x => new
                {
                    Error = x.ErrorMessage,
                    Property = x.PropertyName
                }));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error occured, please contact support" });
            }
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
