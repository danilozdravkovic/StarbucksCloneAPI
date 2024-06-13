using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.API.Core;
using StarbuckClone.API.DTO;
using StarbuckClone.API.Extensions;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.Users;
using StarbucksClone.Application.UseCases.Queries.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        private readonly JwtTokenCreator _tokenCreator;

        public UsersController(UseCaseHandler commandHandler, JwtTokenCreator tokenCreator)
        {
            _commandHandler = commandHandler;
            _tokenCreator = tokenCreator;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearchDto search, [FromServices] ISearchUsersQuery query)
        {
            try
            {
                var result = _commandHandler.HandleQuery(query, search);
                return Ok(result);
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
                _commandHandler.HandleCommand(cmd, dto);

                return StatusCode(201);
        
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] AuthRequest request)
        {
           
                string token = _tokenCreator.Create(request.Email, request.Password);

                return Ok(new AuthResponse { Token = token });
          
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}/access")]
        public IActionResult ChangeAccess(int id, [FromBody] UpdateUserAccessDto dto,[FromServices]IUpdateUserAccessCommand cmd)
        {

                dto.UserId = id;
                _commandHandler.HandleCommand(cmd, dto);
                return NoContent();
       
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Authorize]
        [HttpDelete("logout")]
        public IActionResult Logout([FromServices] ITokenStorage storage)
        {
          
                storage.Remove(Request.GetTokenId().Value);
                return NoContent();
 
        }
    }
}
