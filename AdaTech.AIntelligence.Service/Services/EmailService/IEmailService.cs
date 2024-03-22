using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.EmailService
{
    /// <summary>
    /// Interface for sending emails and confirming user email addresses.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The email address of the recipient.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The body of the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendEmailAsync(string email, string subject, string message);

        /// <summary>
        /// Confirms a user's email address asynchronously.
        /// </summary>
        /// <param name="user">The user whose email address is to be confirmed.</param>
        /// <param name="token">The token used for confirming the email address.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ConfirmEmailAsync(UserInfo user, string token);
    }
}
