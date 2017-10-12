using System.Threading.Tasks;

namespace blog.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string address, string subject, string message);

        Task SendEmailConfirmationAsync(string address, string callbackUrl);
    }
}
