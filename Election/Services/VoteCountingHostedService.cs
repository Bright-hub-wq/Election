using System;
using System.Threading;
using System.Threading.Tasks;
using Election.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Election.Services
{
    public class VoteCountingHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public VoteCountingHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var voteCountingService = scope.ServiceProvider.GetRequiredService<VoteCountingService>();
                    await voteCountingService.CountVotesForEndedElections();
                }

                // Wait before running again (e.g., run every hour)
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
