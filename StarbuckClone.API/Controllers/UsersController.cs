using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.API.Core;
using StarbuckClone.API.DTO;
using StarbuckClone.API.Extensions;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.CartLines;
using StarbucksClone.Application.UseCases.Commands.Users;
using StarbucksClone.Application.UseCases.Queries.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        private readonly JwtTokenCreator _tokenCreator;

        public UsersController(UseCaseHandler useCaseHandler, JwtTokenCreator tokenCreator)
        {
            _useCaseHandler = useCaseHandler;
            _tokenCreator = tokenCreator;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearchDto search, [FromServices] ISearchUsersQuery query)
        {
          
                var result = _useCaseHandler.HandleQuery(query, search);
                return Ok(result);
       
        
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetUserQuery query)
        {
            var result = _useCaseHandler.HandleQuery(query, id);
            return Ok(result);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand cmd)
        {
                _useCaseHandler.HandleCommand(cmd, dto);

                return StatusCode(201);
        
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] AuthRequest request)
        {
           
                string token = _tokenCreator.Create(request.Email, request.Password);

                return Ok(new AuthResponse { Token = token });
          
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ModifyUserDto dto, [FromServices] IModifyUserCommand command)
        {
            dto.Id = id;
            _useCaseHandler.HandleCommand(command, dto);

            return StatusCode(201);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}/access")]
        public IActionResult ChangeAccess(int id, [FromBody] UpdateUserAccessDto dto,[FromServices]IUpdateUserAccessCommand cmd)
        {

                dto.UserId = id;
                _useCaseHandler.HandleCommand(cmd, dto);
                return NoContent();
       
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand command)
        {
            _useCaseHandler.HandleCommand(command, id);
            return NoContent();
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
