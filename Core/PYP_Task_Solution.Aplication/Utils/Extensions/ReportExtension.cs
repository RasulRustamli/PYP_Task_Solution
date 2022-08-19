using Microsoft.EntityFrameworkCore;
using PYP_Task_Solution.Aplication.DTOs;
using PYP_Task_Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Select = PYP_Task_Solution.Aplication.Utils.Enums.ReportType;


namespace PYP_Task_Solution.Aplication.Utils.Extensions
{
    public static class ReportExtension
    {
        public static IQueryable<ReportDto> GetReportByTypeFromDb(this IQueryable<ExcelSheets> query, Select select, DateTime StartDate, DateTime EndDate)
        {

            query = query.Where(d => d.Date >= StartDate && d.Date <= EndDate);

            var groupBy = select switch
            {

                Select.SalesBySegment => query.GroupBy(x => x.Segment),
                Select.SalesByCountry => query.GroupBy(x => x.Country),
                Select.SalesByProduct => query.GroupBy(x => x.Product),
                Select.DiscountByProduct => query.GroupBy(x => x.Product),
                _ => throw new NotImplementedException()
            };

            return groupBy.Select(g => new ReportDto
            {
                Type = g.Key,
                UnitsSold = g.Sum(x => x.UnitsSold),
                GrossSales = g.Sum(x => x.GrossSales),
                Discount = g.Sum(x => x.Discount),
                Profit = g.Sum(x => x.Profit),
                DiscounInterest = select == Select.DiscountByProduct ? g.Sum(x => x.Discount) / g.Sum(x => x.GrossSales) * 100 : 0,
            }).AsNoTracking();


        }
    }
}
