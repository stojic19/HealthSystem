using Hospital.Schedule.Repository;
using Hospital.Schedule.Service.Interfaces;
using Hospital.SharedModel.Repository.Base;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        private readonly IUnitOfWork _uow;
        private IScheduledEventService scheduledEventsService;

        public ConsumeScopedServiceHostedService(IUnitOfWork unitOfWork, IScheduledEventService scheduledEventsService)
        {
            this._uow = unitOfWork;
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
