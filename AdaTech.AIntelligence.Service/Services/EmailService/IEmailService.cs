using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.EmailService
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
        public Task ConfirmEmailAsync(UserInfo user, string token);

    }
}