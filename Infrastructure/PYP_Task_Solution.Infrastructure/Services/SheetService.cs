using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using PYP_Task_Solution.Aplication.DTOs;
using PYP_Task_Solution.Aplication.Services;
using PYP_Task_Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Infrastructure.Services;

public  class SheetService : ISheetService
{
    public async Task<bool> FileAditionalCheckAsync(IFormFile file)
    {
        try
        {
            var fileExtension = Path.GetExtension(file.FileName);
            string contentType = file.ContentType;

            if (fileExtension == ".xlsx" || fileExtension == ".xls" && contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || contentType == "application/vnd.ms-excel")
            {
                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                byte[] xlsxBytes = { 0x50, 0x4B, 0x03, 0x04 };
                byte[] xlsBytes = { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

                if (fileBytes.Take(4).SequenceEqual(xlsxBytes) || fileBytes.Take(8).SequenceEqual(xlsBytes) && fileBytes.Length <= 5242880)
                    return true;
                return false;

            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public bool TemplateValidateCheck(IFormFile file)
    {
        try
        {

            Stream stream = file.OpenReadStream();


            string[] excelColums = {
            "Segment" , "Country", "Product", "Discount Band",
            "Units Sold", "Manufacturing Price","Sale Price" , "Gross Sales",
            "Discounts", "Sales", "COGS" , "Profit" , "Date"
        };

            using ExcelPackage package = new(stream);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
            int rowCount = worksheet.Dimension.Rows;
            int colums = worksheet.Dimension.Columns;

            if (rowCount > 1 && colums == 13)
            {
                for (int col = 1; col <= colums; col++)
                {

                    if (worksheet.Cells[1, col]?.Value?.ToString()?.Trim() != excelColums[col - 1]) return false;

                }
                return true;
            }


            return false;
        }
        catch (Exception ex)
        {
                return false;
        }
       
    }

    public async Task<(bool, bool, bool, List<ExcelSheetsDto> excelSheetsDto)> UploadAsync(IFormFile file)
    {
        List<ExcelSheetsDto> datas=new List<ExcelSheetsDto>();


        if (!await FileAditionalCheckAsync(file)) return (false, false, false, datas);
        if (!TemplateValidateCheck(file)) return (true, false, false, datas);




        Stream stream = file.OpenReadStream();

        using var package = new ExcelPackage(stream);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
        int rowCount = worksheet.Dimension.Rows;


        var noneCount = 0;

        for (var row = 2; row <= rowCount; row++)
        {
            try
            {
                ExcelSheetsDto data = new ExcelSheetsDto();
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

                if (noneCount > 0 || data.UnitsSold == 0 || data.Date == DateTime.MinValue) continue;

                datas.Add(data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return (true, true, true, datas);

    }
}
