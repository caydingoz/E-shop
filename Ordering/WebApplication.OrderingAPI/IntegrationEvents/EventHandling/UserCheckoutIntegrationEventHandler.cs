using DotNetCore.CAP;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.OrderingAPI.Applications.Commands;
using WebApplication.OrderingAPI.IntegrationEvents.Events;

namespace WebApplication.OrderingAPI.IntegrationEvents.EventHandling
{
    public class UserCheckoutIntegrationEventHandler : ICapSubscribe
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserCheckoutIntegrationEventHandler> _logger;

        public UserCheckoutIntegrationEventHandler(IMediator mediator, ILogger<UserCheckoutIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [CapSubscribe(nameof(UserCheckoutIntegrationEvent))]
        public async Task Handle(UserCheckoutIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: OrderinAPI - ({@IntegrationEvent})", @event);

            var createOrderCommand = new CreateOrderCommand(@event.Basket.Items, @event.BuyerId, @event.BuyerName,
                @event.Adress, @event.CardNumber);

            bool result = await _mediator.Send(createOrderCommand);

            if (result)
            {
                _logger.LogInformation("CreateOrderCommand suceeded - BuyerId: {BuyerId}", @event.BuyerId);
            }
            else
            {
                _logger.LogWarning("CreateOrderCommand failed - BuyerId: {BuyerId}", @event.BuyerId);
            }
        }
    }
}
