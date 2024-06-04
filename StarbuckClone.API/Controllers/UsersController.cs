using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _commandHandler;

        public UsersController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand cmd)
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
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error occured, please contact support" });
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
