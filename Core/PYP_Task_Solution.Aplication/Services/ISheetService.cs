using Microsoft.AspNetCore.Http;
using PYP_Task_Solution.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Services
{
    public interface ISheetService
    {
        
        bool TemplateValidateCheck(IFormFile file);
        Task<(bool, bool, bool, List<ExcelSheetsDto> excelSheetsDto)> UploadAsync(IFormFile file);
        
        Task<bool> FileAditionalCheckAsync(IFormFile file);
    }
}
