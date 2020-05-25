using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Hosting;
using Gov.Structure.Config;

namespace Gov.Structure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly MailConfig _emailSettings;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger _logger;

        public EmailSender(
            IOptions<MailConfig> emailSettings,
            IWebHostEnvironment env, ILoggerFactory logger)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
            _logger = logger.CreateLogger("EmailSender"); 
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.senderName, _emailSettings.mailFrom));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                     if (_env.EnvironmentName == "Development")
                    {                      
                        await client.ConnectAsync(_emailSettings.host, _emailSettings.portNo, SecureSocketOptions.StartTlsWhenAvailable);
                    }
                    else
                    {
                        await client.ConnectAsync(_emailSettings.host);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(_emailSettings.mailFrom, _emailSettings.password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("impossibile inviare email all'account " + email + " dettagli : " + ex.Message);
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
