using Hospital.Schedule.Service.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
     
        private IScheduledEventService scheduledEventsService;
        public ConsumeScopedServiceHostedService(IScheduledEventService scheduledEventsService)
        {
           
            this.scheduledEventsService = scheduledEventsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               System.Diagnostics.Debug.WriteLine("-- background service running --");

                scheduledEventsService.UpdateFinishedUserEvents();           
                await Task.Delay(60000, stoppingToken);
            }
        }
      
    }
}
