using PYP_Task_Solution.Domain.Entities;
using PYP_Task_Solution.Persistence.Contexts;
using PYP_Task_Solution.Aplication.Repositories.ExcelRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Persistence.Repositories.ExcelRepository;

public class ExcelRepository : Repository<ExcelSheets> , IExcelRepository
{
    public ExcelRepository(PYP_Task_SolutionDbContext context) : base(context)
    {
    }
}
