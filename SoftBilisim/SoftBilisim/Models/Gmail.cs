using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SoftBilisim.Models
{
    public static class Gmail
    {
        public static void SendMail(string body)
        {
            var fromAddress = new MailAddress("merveyilmaz669@gmail.com", "Soft Bilisim Geribildirim");
            var toAddress = new MailAddress("merveyilmaz669@gmail.com");
            const string subject = "Soft Bilisim Geribildirim";

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "123456")
                //123456 kısmı e-posta adresinin şifresinin yazılacağı kısım
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}