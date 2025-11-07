namespace WebApiTest.Services
{
    public interface INotificationService
    {
        public Task<bool> SendEmail(string to, string subject, string body);
    }
}
