using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class SmtpEmailSender : IEmailSenderStrategy
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly string _emailTemplateFilePath = "..\\EmailService\\Templates\\EmailTemplate.html";

        public SmtpEmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async void SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var emailBody = File.ReadAllText(_emailTemplateFilePath);
            emailBody = emailBody.Replace("{{header}}", emailMessage.Subject);
            emailBody = emailBody.Replace("{{receiver}}", message.To.First().Address);
            emailBody = emailBody.Replace("{{link}}", message.Content);

            var bodyBuilder = new BodyBuilder { HtmlBody = emailBody };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
