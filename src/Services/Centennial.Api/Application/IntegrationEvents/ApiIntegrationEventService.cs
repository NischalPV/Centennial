using System;
using System.Data.Common;
using System.Threading.Tasks;
using Centennial.Api.Data;
using Centennial.BuildingBlocks.EventBus.Abstractions;
using Centennial.BuildingBlocks.EventBus.Events;
using Centennial.BuildingBlocks.IntegrationEventLogEF.Services;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Application.IntegrationEvents
{
    public class ApiIntegrationEventService: IApiIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly CentennialDbContext _dbContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<ApiIntegrationEventService> _logger;

        public ApiIntegrationEventService(
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            IEventBus eventBus,
            CentennialDbContext dbContext,
            IIntegrationEventLogService eventLogService,
            ILogger<ApiIntegrationEventService> logger)
        {
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await _eventLogService.SaveEventAsync(evt, _dbContext.GetCurrentTransaction());
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, Program.AppName);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }
    }
}
