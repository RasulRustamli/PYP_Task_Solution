using AutoMapper;
using MediatR;
using PYP_Task_Solution.Aplication.DTOs;
using PYP_Task_Solution.Aplication.Repositories.ExcelRepository;
using PYP_Task_Solution.Aplication.Services;
using PYP_Task_Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Features.Commands;

public class ExcelToDbCommandHandler : IRequestHandler<ExcelToDbCommandRequest, ExcelToDbCommandResponse>
{
    private readonly ISheetService _sheetService;
    private readonly IExcelRepository _excelRepository;
    private readonly IMapper _mapper;
    public ExcelToDbCommandHandler(ISheetService sheetService,IExcelRepository excelRepository,IMapper mapper)
    {
        _sheetService = sheetService;
        _excelRepository = excelRepository;
        _mapper = mapper;
    }
    public async Task<ExcelToDbCommandResponse> Handle(ExcelToDbCommandRequest request, CancellationToken cancellationToken)
    {
        (bool IsXlsxOrXls, bool TemplateValidate, bool UploadSuccest, List<ExcelSheetsDto> datas) = await _sheetService.UploadAsync(request.formFile);

        if (datas != null)
        {
            List<ExcelSheets> spreadData = _mapper.Map<List<ExcelSheets>>(datas);
            await _excelRepository.AddRangeAsync(spreadData);
            await _excelRepository.SaveAsync();
        }
        return new()
        {
            Success = IsXlsxOrXls || TemplateValidate || UploadSuccest == true ? true : false,
            Message = IsXlsxOrXls != true ? "Corupted file type" :
            TemplateValidate != true ? "File template is not suitable" :
            UploadSuccest != true ? "An error occurred while reading the file" :
            "File information successfully saved"
        };

    }
}

