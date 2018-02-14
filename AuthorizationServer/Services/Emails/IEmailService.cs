using System.Threading.Tasks;

namespace AuthorizationServer.Services.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
