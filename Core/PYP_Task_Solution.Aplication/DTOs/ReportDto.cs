using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.DTOs;

public class ReportDto
{
    public string Type { get; set; }
    public double UnitsSold { get; set; }
    public double GrossSales { get; set; }

    public double Discount { get; set; }
    public double Profit { get; set; }
    public double DiscounInterest { get; set; }
}
