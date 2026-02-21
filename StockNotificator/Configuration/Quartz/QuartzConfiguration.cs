using Quartz;
using StockNotificator.Application.Jobs.AlertJob;

namespace StockNotificator.Configuration.Quartz
{
    public static class QuartzConfiguration
    {
        public static IServiceCollection ConfigureQuartz(this IServiceCollection services)
        {
            services.ConfigureAlertJob();
            services.AddQuartzHostedService(x => x.WaitForJobsToComplete = true);
            return services;
        }
    }
}
