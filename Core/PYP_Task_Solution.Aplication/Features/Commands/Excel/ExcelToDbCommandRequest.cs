using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Features.Commands
{
    public class ExcelToDbCommandRequest: IRequest<ExcelToDbCommandResponse>
    {
        public IFormFile formFile { get; set; }
    }
}
