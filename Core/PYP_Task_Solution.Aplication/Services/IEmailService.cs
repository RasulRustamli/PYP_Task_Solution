using PYP_Task_Solution.Aplication.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Services
{
    public interface IEmailService
    {
        public Task<bool> ReportSendEmail(string[] email,string filePath,ReportType reportType);
    }
}
