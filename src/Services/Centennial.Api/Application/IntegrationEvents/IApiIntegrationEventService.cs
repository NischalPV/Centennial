using System;
using System.Threading.Tasks;
using Centennial.BuildingBlocks.EventBus.Events;

namespace Centennial.Api.Application.IntegrationEvents
{
    public interface IApiIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
