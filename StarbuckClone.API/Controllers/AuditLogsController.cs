using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarbuckClone.Implementation;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.AuditLogs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarbuckClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private UseCaseHandler _commandHandler;

        public AuditLogsController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }
        // GET: api/<AuditLogsController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] AuditLogSearchDto search , [FromServices] ISearchAuditLogsQuery query)
        {
            try
            {
                var result=_commandHandler.HandleQuery(query, search);

                return Ok(result);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error occured, please contact support" });
            }
        }

        // GET api/<AuditLogsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuditLogsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuditLogsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuditLogsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
