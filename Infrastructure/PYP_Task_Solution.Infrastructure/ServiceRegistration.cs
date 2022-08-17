using Microsoft.Extensions.DependencyInjection;
using PYP_Task_Solution.Aplication.Services;
using PYP_Task_Solution.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ISheetService, SheetService>();
        }
    }
}
