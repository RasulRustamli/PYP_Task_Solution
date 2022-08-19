using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PYP_Task_Solution.Aplication.Features.Commands;
using PYP_Task_Solution.Aplication.Features.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PYP_Task_Solution.WebApi.Controllers;

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
    public async Task<IActionResult> Post(IFormFile fromFile)
    {
        ExcelToDbCommandRequest excelToDbCommandRequest = new();
        excelToDbCommandRequest.formFile = fromFile;
        ExcelToDbCommandResponse response = await _mediator.Send(excelToDbCommandRequest);

        return StatusCode((int)HttpStatusCode.OK, response);
    }

        
}
