using System.Net;
using System.Net.Mail;
using AmaselBE.Configuration;
using AmaselBE.Model;

namespace AmaselBE.Services
{
    public class UtilService
    {
        public static string SmtpUsername { get; set; }
        public static string SmtpPassword { get; set; }
        public static string HostName { get; set; }
        public MailLogService mailLogService { get; set; }
        public UtilService(Setting setting)
        {
            SmtpUsername = setting.Username;
            SmtpPassword = setting.Password;
            HostName = setting.HostName;
            mailLogService = new MailLogService(setting);
        }
        public void sendMail(Mail message, string entity)
        {
            var smtpClient = new SmtpClient(HostName)
            {
                Port = 587,
                Credentials = new NetworkCredential(SmtpUsername, SmtpPassword),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(SmtpUsername),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(message.Recipient);
            var log = new MailLog();
            log.Entity = entity;
            log.Time = DateTime.Now;
            log.Content = message.Body;
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error = ex.Message;
            }
            finally
            {
                List<MailLog> logs = new List<MailLog>();
                logs.Add(log);
                mailLogService.Save(logs);
            }
            // return "Mail sent successfully";
        }

    }

}