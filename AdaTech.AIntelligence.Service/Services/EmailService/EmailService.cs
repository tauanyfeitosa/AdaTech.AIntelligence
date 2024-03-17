using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace AdaTech.AIntelligence.Service.Services.EmailService
{
    /// <summary>
    /// Service for sending emails and confirming user email addresses.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _appSettings;
        private readonly UserManager<UserInfo> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="smtpClient">The SMTP client for sending emails.</param>
        /// <param name="userManager">The user manager for managing user entities.</param>
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

        /// <inheritdoc/>
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The email address of the recipient.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The body of the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <inheritdoc/>
        /// <summary>
        /// Confirms a user's email address asynchronously.
        /// </summary>
        /// <param name="user">The user whose email address is to be confirmed.</param>
        /// <param name="token">The token used for confirming the email address.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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
