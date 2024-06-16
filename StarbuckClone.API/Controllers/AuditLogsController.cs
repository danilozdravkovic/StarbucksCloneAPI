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
        private UseCaseHandler _useCaseHandler;

        public AuditLogsController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }
        // GET: api/<AuditLogsController>
        
        [HttpGet]
        public IActionResult Get([FromQuery] AuditLogSearchDto search , [FromServices] ISearchAuditLogsQuery query)
        {
                var result=_useCaseHandler.HandleQuery(query, search);

                return Ok(result);
        }

       
    }
}
