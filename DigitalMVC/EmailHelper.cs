using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace DigitalMVC
{
    public class EmailHelper
    {
        //Marksmen12#1206704!1
        public static void SendEmail(string userName, string toEmail)
        {
            // string emai
            var fromAddress = new MailAddress("jeremy.codef@gmail.com", "TerroBoards");
            var toAddress = new MailAddress("toEmail", userName);
            const string fromPassword = "Marksmen12#1206704!1";
            const string subject = "test";
            const string body = "Hey now!!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
