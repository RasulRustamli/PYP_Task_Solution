using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PYP_Task_Solution.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExcelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/<ExcelController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

            
    }
}
