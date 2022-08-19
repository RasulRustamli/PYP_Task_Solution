using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PYP_Task_Solution.Aplication.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceRegistration));
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
