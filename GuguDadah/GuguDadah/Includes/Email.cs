using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GuguDadah.Includes {

    public class Email {

        public void SendEmail(string destination, string subject, string body) {

            SmtpClient client = new SmtpClient("smtp.sapo.pt");
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("gugudadah@sapo.pt", "Securityhole!");

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("gugudadah@sapo.pt");
            mailMessage.To.Add(destination);
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            client.Send(mailMessage);
        }

    }
}
