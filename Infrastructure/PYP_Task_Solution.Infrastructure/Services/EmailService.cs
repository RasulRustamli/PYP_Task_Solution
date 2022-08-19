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

namespace PYP_Task_Solution.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> ReportSendEmail(string[] email, string filePath)
        {
            var client = new SendGridClient(Configuration.EmailConfiguration["ApiKey"]);
            var from = new EmailAddress(Configuration.EmailConfiguration["From"], "Rasul Rustamli");
            var subject = "Report Statistics";
            var to = new List<EmailAddress>();
            email.ToList().ForEach(x => to.Add(new EmailAddress(x)));
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, "Report Statistics", null);
            using var fileStream = File.OpenRead(filePath);
            await msg.AddAttachmentAsync(filePath.Substring(filePath.LastIndexOf("/") + 1), fileStream);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode == true ? true : false;
        }
    }
}
