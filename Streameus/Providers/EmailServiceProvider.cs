using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Streameus.Providers
{
    /// <summary>
    /// Service to send emails
    /// </summary>
    public class EmailServiceProvider : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            const string credentialUserName = "streameus@gmail.com";
            const string sentFrom = "streameus@gmail.com";
            const string pwd = "123tustream";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 465,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

            // Creatte the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new System.Net.Mail.MailMessage(sentFrom, message.Destination)
                {
                    Subject = message.Subject,
                    Body = message.Body
                };

            // Send:
            return client.SendMailAsync(mail);
        }
    }
}