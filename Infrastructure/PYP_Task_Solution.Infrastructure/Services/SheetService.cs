using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using PYP_Task_Solution.Aplication.Services;
using PYP_Task_Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Infrastructure.Services;

public class SheetService : ISheetService
{
    public bool TemplateValidateCheck(IFormFile file)
    {
        Stream stream = file.OpenReadStream();

        using ExcelPackage package = new(stream);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
        int rowCount = worksheet.Dimension.Rows;
        int colums = worksheet.Dimension.Columns;

        if (rowCount > 1 && colums == 13) return true;

        return false;
    }

    public async Task<bool> UploadAsync(IFormFile file)
    {
        var a = TemplateValidateCheck(file);
        if (!TemplateValidateCheck(file)) return false;

        var stream = file.OpenReadStream();

        using var package = new ExcelPackage(stream);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
        int rowCount = worksheet.Dimension.Rows;

        List<ExcelSheets> datas = new List<ExcelSheets>();

        var noneCount = 0;

        for (var row = 2; row <= rowCount; row++)
        {
            try
            {
                ExcelSheets data = new ExcelSheets();
                double defaultValue = 0;
                DateTime defaultDate = DateTime.MinValue;
                data.Segment = worksheet.Cells[row, 1]?.Value?.ToString()?.Trim() ?? $"None{noneCount++}";   
                data.Country = worksheet.Cells[row, 2]?.Value?.ToString()?.Trim() ?? $"None{noneCount++}";
                data.Product = worksheet.Cells[row, 3]?.Value?.ToString()?.Trim() ?? $"None{noneCount++}";
                data.DiscountBand = worksheet.Cells[row, 4]?.Value?.ToString()?.Trim() ?? "None";


                data.UnitsSold = double.TryParse(worksheet.Cells[row, 5]?.Value?.ToString()?.Trim(), out defaultValue) == true ? defaultValue : 0;
                data.ManufacturingPrice = double.TryParse(worksheet.Cells[row, 6]?.Value?.ToString()?.Trim(), out defaultValue) == true ? defaultValue : 0;
                data.SellPrice = double.TryParse(worksheet.Cells[row, 7]?.Value?.ToString()?.Trim(), out defaultValue) == true ? defaultValue : 0;
                data.GrossSales = double.TryParse(worksheet.Cells[row, 8]?.Value?.ToString()?.Trim(), out defaultValue) == true ? defaultValue : 0;
                data.Discount = double.TryParse(worksheet.Cells[row, 9]?.Value?.ToString()?.Trim(), out defaultValue) == true ? defaultValue : 0;
                data.Sale = double.TryParse(worksheet.Cells[row, 10]?.Value?.ToString()?.Trim(), out defaultValue) == true ? defaultValue : 0;
                data.COGS = double.TryParse(worksheet.Cells[row, 11].Value?.ToString()?.Trim(), out defaultValue) == true ? defaultValue : 0;
                data.Profit = double.TryParse(worksheet.Cells[row, 12]?.Value?.ToString()?.Trim(), out defaultValue) == true ? defaultValue : 0;

                data.Date = DateTime.TryParse(worksheet.Cells[row, 13]?.Value?.ToString()?.Trim(), out defaultDate) == true ? defaultDate : DateTime.MinValue;

                if (  noneCount>0 || data.UnitsSold == 0 || data.Date == DateTime.MinValue) continue;

                datas.Add(data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return true;
    }


}
