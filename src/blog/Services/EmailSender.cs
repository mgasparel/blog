using System.Threading.Tasks;

namespace blog.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string address, string subject, string message)
            => Task.CompletedTask;

        public Task SendEmailConfirmationAsync(string address, string callbackUrl)
            => Task.CompletedTask;
    }
}
