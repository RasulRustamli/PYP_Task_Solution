using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PYP_Task_Solution.Aplication.Repositories.ExcelRepository;
using PYP_Task_Solution.Persistence.Contexts;
using PYP_Task_Solution.Persistence.Repositories.ExcelRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        #region Connection String
        services.AddDbContext<PYP_Task_SolutionDbContext>(options => options.UseSqlServer(Configuration.ConnectionString, b => b.MigrationsAssembly(typeof(PYP_Task_SolutionDbContext).Assembly.FullName)));
        #endregion
        services.AddScoped<IExcelRepository, ExcelRepository>();

    }
}
