using MediatR;
using PYP_Task_Solution.Aplication.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Features.Queries;

public class ReportQueryRequest:IRequest<ReportQueryResponse>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string[] AcceptorEmail { get; set; }
    public ReportType ReportType { get; set; }
}
