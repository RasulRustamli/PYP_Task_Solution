using PYP_Task_Solution.Aplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using PYP_Task_Solution.Persistence;
using PYP_Task_Solution.Aplication.Utils.Enums;
using Microsoft.Extensions.Logging;

namespace PYP_Task_Solution.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _log;
        public async Task<bool> ReportSendEmail(string[] email, string filePath,ReportType reportType)
        {
            var client = new SendGridClient(Configuration.EmailConfiguration["ApiKey"]);
            var from = new EmailAddress(Configuration.EmailConfiguration["From"], "Rasul Rustamli");
            var subject = "Report Statistics";
            var to = new List<EmailAddress>();
            email.ToList().ForEach(x => to.Add(new EmailAddress(x)));
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, "Report Statistics", null);
            using var fileStream = File.OpenRead(filePath);
            await msg.AddAttachmentAsync(filePath.Substring(filePath.LastIndexOf("/") + 1), fileStream);
            try
            {
                var response = await client.SendEmailAsync(msg);
                if (response.IsSuccessStatusCode != true)
                {
                    _log.LogInformation("Send Raport: {Email}:{RapartType}:{SendRaportDate}", email, reportType.ToString(), DateTime.Now);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                _log.LogError("Error in email services", ex);
                return false;
            }
        }
    }
}
