using Hospital.Schedule.Service.Interfaces;
using Hospital.SharedModel.Repository.Base;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        private readonly IUnitOfWork uow;
        private IScheduledEventService scheduledEventsService;
        public ConsumeScopedServiceHostedService(IUnitOfWork unitOfWork, IScheduledEventService scheduledEventsService)
        {
            this.uow = unitOfWork;
            this.scheduledEventsService = scheduledEventsService;       
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await UpdateFinishedUserEvents(stoppingToken);
        }

        private async Task UpdateFinishedUserEvents(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                scheduledEventsService.UpdateFinishedUserEvents();
                await Task.Delay(60000, cancellationToken);
            }
        }
    }
}
