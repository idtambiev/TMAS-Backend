using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.DTO;
using TMAS.DB.Models;
using MimeKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;
using TMAS.BLL.Interfaces;
using TMAS.BLL.Models;

namespace TMAS.BLL.Services
{
    public class EmailService:IEmailService
    {
        public EmailService()
        {
        }

        public async Task SendEmailAsync(EmailOptions emailOptions)
        {
            var fromAddress = new MailAddress("davearmstrong653@gmail.com", "Dave Armstrong");
            var toAddress = new MailAddress(emailOptions.Email, "Idris");
            string fromPassword = "009090909";
            string subject = emailOptions.Subject;
            string body = emailOptions.Content;

            var smtp = new System.Net.Mail.SmtpClient
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
                Body = body,
                IsBodyHtml=true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
