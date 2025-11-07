using Hangfire;
using WebApiTest.Services;

namespace WebApiTest.Endpoints
{
    public static class JobsEndpoints
    {
        public static WebApplication AddJobsEndpoints(this WebApplication app)
        {
            var to = "ralphneto@gmail.com";
            var subject = "WebAPITest Email";

            app.MapGet("fire-and-forget", (INotificationService notificationService, IBackgroundJobClient client) => {
                client.Enqueue(() => notificationService.SendEmail(to, subject, "Fire-And-Forget"));
            });

            app.MapGet("fire-and-forget-continuation", (INotificationService notificationService, IBackgroundJobClient client) => {
                var id = client.Enqueue(() => notificationService.SendEmail(to, subject, "Fire-And-Forget"));

                client.ContinueJobWith(id, () => notificationService.SendEmail(to, subject, "Continuation"));
            });

            app.MapGet("fire-and-forget-delayed", (INotificationService notificationService, IBackgroundJobClient client) => {
                client.Schedule(() => notificationService.SendEmail(to, subject, "Fire-And-Forget Delayed"), TimeSpan.FromSeconds(5));
            });
            
            app.MapGet("recurring", (INotificationService notificationService, IRecurringJobManager manager) => {
                //var currentMinute = DateTime.Now.Minute + 1;

                //var cronExpression = $"*/{currentMinute} * * * *"; //Every currentMinute in hour
                var cronExpression = $"* * * * *"; //Every Minute

                manager.AddOrUpdate($"recurring-job-{Guid.NewGuid()}", () => notificationService.SendEmail(to, subject, "Recurring"), cronExpression);
            });

            app.MapGet("cancel-job/{id}", (string id, IRecurringJobManager manager) => {
                manager.RemoveIfExists(id);
            });

            return app;
        }
    }
}