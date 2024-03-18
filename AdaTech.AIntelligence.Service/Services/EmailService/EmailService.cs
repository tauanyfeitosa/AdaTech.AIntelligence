using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace AdaTech.AIntelligence.Service.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _appSettings;
        private readonly UserManager<UserInfo> _userManager;

        public EmailService(IConfiguration appSettings, SmtpClient smtpClient, UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
            _appSettings = appSettings;
            _smtpClient = smtpClient;
            _smtpClient.Port = 587;
            _smtpClient.Credentials = new System.Net.NetworkCredential(appSettings.GetValue<string>("ServerSMTP:Smtp:User"), appSettings.GetValue<string>("ServerSMTP:Smtp:Password"));
            _smtpClient.Host = appSettings.GetValue<string>("ServerSMTP:Smtp:Host");
            _smtpClient.EnableSsl = true;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_appSettings.GetValue<string>("ServerSMTP:Smtp:User")),
                };
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;

                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro ao enviar email: {ex}");
            }
        }

        public async Task ConfirmEmailAsync(UserInfo user, string token)
        {
            try
            {
                await _userManager.ConfirmEmailAsync(user, token);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro ao confirmar email: {ex}");
            }
        }
    }
}
