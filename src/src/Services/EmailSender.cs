using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        //dependency injection
        public SendGridOptions _sendGridOptions { get; }
        public IDotnetdesk _dotnetdesk { get; }
        public SmtpOptions _smtpOptions { get; }
        public EmailSender(IOptions<SendGridOptions> sendGridOptions,
                IDotnetdesk dotnetdesk,
                IOptions<SmtpOptions> smtpOptions)
        {
            _sendGridOptions = sendGridOptions.Value;
            _dotnetdesk = dotnetdesk;
            _smtpOptions = smtpOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //send email using sendgrid via dotnetdesk
            _dotnetdesk.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey, 
                _sendGridOptions.FromEmail, 
                _sendGridOptions.FromFullName, 
                subject, 
                message, 
                email).Wait();

            //send email using smtp via dotnetdesk. uncomment to use it
            /*
            _dotnetdesk.SendEmailByGmailAsync(_smtpOptions.fromEmail,
                _smtpOptions.fromFullName,
                subject,
                message,
                email,
                email,
                _smtpOptions.smtpUserName,
                _smtpOptions.smtpPassword,
                _smtpOptions.smtpHost,
                _smtpOptions.smtpPort,
                _smtpOptions.smtpSSL).Wait();
                */
            return Task.CompletedTask;
        }
    }
}
