using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PYP_Task_Solution.Persistence.Contexts;
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

    }
}
