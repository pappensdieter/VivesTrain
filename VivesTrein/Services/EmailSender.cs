using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace VivesTrein.Services
{
    public class EmailSender
    {
        public async Task ReceiveEmailAsync(string email, string subject, string mesg)
        {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress("InfoVivesTrein@gmail.com"));
            mail.From = new MailAddress(email);

            mail.Subject = subject;
            mail.Body = mesg;
            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("InfoVivesTrein@gmail.com", "Azerty-123");
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task SendEmailAsync(string email, string subject, string mesg)
        {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress("InfoVivesTrein@gmail.com");

            mail.Subject = subject;
            mail.Body = mesg;
            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("InfoVivesTrein@gmail.com", "Azerty-123");
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
