using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Persistence.Contexts
{
    public class PYP_Task_SolutionDbContext:DbContext
    {
        public PYP_Task_SolutionDbContext(DbContextOptions options):base(options)
        {

        }
    }
}
