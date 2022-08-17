using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Services
{
    public interface ISheetService
    {
        Task<bool> UploadAsync(IFormFile file);
        bool TemplateValidateCheck(IFormFile file);
    }
}
