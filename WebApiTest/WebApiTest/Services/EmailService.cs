
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;

namespace WebApiTest.Services
{
    public class EmailService : INotificationService
    {
        public Task<bool> SendEmail(string to, string subject, string body)
        {
            System.Diagnostics.Debug.WriteLine($"Email enviado para {to}, título: {subject}, corpo: {body}");

            return Task.FromResult(true);
        }
    }
}
