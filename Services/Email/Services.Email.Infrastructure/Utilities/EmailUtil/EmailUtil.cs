using Services.Email.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Infrastructure.Utilities.EmailUtil
{
    public static class EmailUtil
    {
        public static string SmtpHost;
        public static int SmtpPort;
        public static string SmtpEmail;
        public static string SmtpPassword;
        public static string SmtpSystemName;

        public static void Initialize()
        {
            SmtpHost = ConfigurationUtil.GetValue<string>("Email:Host");
            SmtpPort = ConfigurationUtil.GetValue<int>("Email:Port");
            SmtpEmail = ConfigurationUtil.GetValue<string>("Email:Email");
            SmtpPassword = ConfigurationUtil.GetValue<string>("Email:Password");
            SmtpSystemName = ConfigurationUtil.GetValue<string>("Email:SystemName");
        }

        public static async Task SendEmail(SendEmailDetails emailDetails)
        {
            await SendMailMessage(await ConstructMailMessage(emailDetails));
        }

        private static async Task<MailMessage> ConstructMailMessage(SendEmailDetails emailDetails)
        {
            MailMessage mailMessage = new MailMessage();

            foreach (string toEmail in emailDetails.ToEmails)
            {
                if (!string.IsNullOrWhiteSpace(toEmail))
                    mailMessage.To.Add(new MailAddress(toEmail));
            }

            foreach (string CcEmail in emailDetails.CcEmails)
            {
                if (!string.IsNullOrWhiteSpace(CcEmail))
                    mailMessage.CC.Add(new MailAddress(CcEmail));
            }

            foreach (string bccEmail in emailDetails.BccEmails)
            {
                if (!string.IsNullOrWhiteSpace(bccEmail))
                    mailMessage.Bcc.Add(new MailAddress(bccEmail));
            }

            mailMessage.Subject = emailDetails.Subject;
            mailMessage.Body = emailDetails.Content;
            mailMessage.From = new MailAddress(SmtpEmail, SmtpSystemName);
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.IsBodyHtml = true;

            if (emailDetails.Attachments != null)
            {
                foreach (var attachment in emailDetails.Attachments)
                {
                    var stream = new MemoryStream(attachment.FileBytes);
                    mailMessage.Attachments.Add(new Attachment(stream, attachment.FileName, attachment.ContentType));
                }
            }

            return mailMessage;
        }

        private static async Task SendMailMessage(MailMessage message)
        {
            using var smtpClient = new SmtpClient
            {
                Host = SmtpHost,
                Port = SmtpPort,
                //EnableSsl = true, // see next tip
                EnableSsl = false,
                Credentials = new NetworkCredential(SmtpEmail, SmtpPassword)
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
