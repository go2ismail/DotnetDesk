using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using src.Data;
using src.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace src.Services
{
    public class Dotnetdesk : IDotnetdesk
    {
        public async Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, email));
            await client.SendEmailAsync(msg);
            
        }

        public async Task SendEmailByGmailAsync(string fromEmail, 
            string fromFullName, 
            string subject, 
            string messageBody, 
            string toEmail, 
            string toFullName, 
            string smtpUser, 
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL)
        {
            var body = messageBody;
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, toFullName));
            message.From = new MailAddress(fromEmail, fromFullName);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                await smtp.SendMailAsync(message);

            }
            
        }

        public async Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager)
        {
            bool result = false;
            try
            {
                var user = await userManager.FindByNameAsync(email);
                if (user != null)
                {
                    //Add this to check if the email was confirmed.
                    if (await userManager.IsEmailConfirmedAsync(user))
                    {
                        result = true;
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public Task CreateDefaultOrganization(string applicationUserId,
            ApplicationDbContext context)
        {
            Organization org = new Organization();
            org.organizationName = "Default HQ";
            org.description = "Default Organization / Default Branch or HQ";
            org.organizationOwnerId = applicationUserId;
            context.Organization.Add(org);
            context.SaveChanges();
            return Task.CompletedTask;
        }
        
        public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, "uploads");
            var extension = "";
            var filePath = "";
            var fileName = "";


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    extension = System.IO.Path.GetExtension(formFile.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;

                }
            }
            
            return result;
        }
    }
}
