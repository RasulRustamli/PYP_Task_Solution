using FluentValidation;
using PYP_Task_Solution.Aplication.Features.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Validator;

public class ReportValidator:AbstractValidator<ReportQueryRequest>
{
    public ReportValidator()
    {
        RuleForEach(e => e.AcceptorEmail)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("not email")
            .Matches("^[a-z0-9](\\.?[a-z0-9]){5,}@code\\.edu\\.az$")
            .WithMessage("Only @code.edu.az domain are accepted");

        RuleFor(r => r.ReportType).IsInEnum();

        RuleFor(d => d.StartDate)
            .NotEmpty()
            .WithMessage("StartDate cannot empty");
        RuleFor(d => d.EndDate)
            .NotEmpty()
            .WithMessage("EndDate cannot Empty");

            

    }
}
