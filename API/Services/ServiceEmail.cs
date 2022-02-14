using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Services
{
    public class RandomGenerator
    {
        private Random Rand = new Random();
        public int GenerateRandom()
        {
            return Rand.Next(1000, 9999);
        }
    }
    public class ServiceEmail
    {
        public void SendEmail(string sendEmail, string message)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.To.Add(new MailAddress(sendEmail));
            mail.From = new MailAddress("mcc.net.batch.60@gmail.com", "IT SUPPORT HELPDESK");
            mail.Subject ="IT Support Helpdesk" + DateTime.Now.ToString();
            mail.Body = "Dear User, <br><br><b>" + message + "</b><br><br> Thank you, <br> IT Support Helpdesk";
            mail.IsBodyHtml = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("mcc.net.batch.60@gmail.com", "mccbatch60123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

        }
    }
}
