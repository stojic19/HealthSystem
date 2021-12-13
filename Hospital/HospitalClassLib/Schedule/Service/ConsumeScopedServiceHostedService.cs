using Hospital.Schedule.Service.ServiceInterface;
using Hospital.SharedModel.Repository.Base;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {

        private readonly IUnitOfWork uow;
        private IScheduledEventsService scheduledEventsService;
        public ConsumeScopedServiceHostedService(IUnitOfWork unitOfWork, IScheduledEventsService scheduledEventsService)
        {
            this.uow = unitOfWork;
            this.scheduledEventsService = scheduledEventsService;
       
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await doWork(stoppingToken);
        }

        private async Task doWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                System.Diagnostics.Debug.WriteLine("OOGa BOOGa");
                scheduledEventsService.updateFinishedUserEvents();
                await Task.Delay(60000);
            }
        }
    }
}
