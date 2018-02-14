using System.Threading.Tasks;

namespace AuthorizationServer.Services.Emails
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
