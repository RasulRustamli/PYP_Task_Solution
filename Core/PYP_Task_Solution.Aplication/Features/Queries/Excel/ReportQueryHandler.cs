﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PYP_Task_Solution.Aplication.Repositories.ExcelRepository;
using PYP_Task_Solution.Aplication.Utils.Extensions;
using PYP_Task_Solution.Aplication.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PYP_Task_Solution.Aplication.Services;

namespace PYP_Task_Solution.Aplication.Features.Queries
{
    public class ReportQueryHandler : IRequestHandler<ReportQueryRequest, ReportQueryResponse>
    {
        private readonly IExcelRepository _excelRepository;
        private readonly ISheetService _sheetService;
        public ReportQueryHandler(IExcelRepository excelRepository,ISheetService sheetService)
        {
            _excelRepository = excelRepository;
            _sheetService = sheetService;
        }

        public  async Task<ReportQueryResponse> Handle(ReportQueryRequest request, CancellationToken cancellationToken)
        {
            List<ReportDto> reportDto = await _excelRepository.Table
           .GetReportByTypeFromDb(request.ReportType, request.StartDate, request.EndDate)
           .ToListAsync();

            (string? filePath, string? fileDirectory) = await _sheetService.CreateExcelFileAsync(request.ReportType, reportDto);

            if (fileDirectory == null) return new() { };

            return new() { };
        }
    }
}