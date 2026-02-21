using Quartz;

namespace StockNotificator.Application.Jobs.AlertJob
{
    public static class AlertJobConfiguration
    {

        public static void ConfigureAlertJob(this IServiceCollection services)
            {
                services.AddQuartz( q =>
                {
                    var jobKey = new JobKey("AlertJob");

                    q.AddJob<AlertJob>(opts => opts.WithIdentity(jobKey));

                    q.AddTrigger(opts => opts
                        .ForJob(jobKey)
                        .WithIdentity("AlertJob-trigger")
                        //.WithCronSchedule("0 0 10-18 ? * MON-FRI"));
                        .WithCronSchedule("0 0/1 * ? * *"));
                });
        }

    }
}
