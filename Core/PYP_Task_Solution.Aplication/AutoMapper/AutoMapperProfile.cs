using AutoMapper;
using PYP_Task_Solution.Aplication.DTOs;
using PYP_Task_Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.AutoMapper
{
    internal class AutoMapperProfile:Profile 
    {
        public AutoMapperProfile()
        {
            CreateMap<ExcelSheetsDto,ExcelSheets>().ReverseMap();
        }
    }
}
