using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.BasketAPI.IntegrationEvents.Events;
using WebApplication.BasketAPI.Model;

namespace WebApplication.BasketAPI.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventHandler : ICapSubscribe
    {
        private readonly ILogger<ProductPriceChangedIntegrationEventHandler> _logger;
        private readonly IBasketRepository _repository;

        public ProductPriceChangedIntegrationEventHandler(ILogger<ProductPriceChangedIntegrationEventHandler> logger,
            IBasketRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [CapSubscribe(nameof(ProductPriceChangedIntegrationEvent))]
        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: BasketAPI - ({@IntegrationEvent})",  @event);
            var userIds = _repository.GetUsers();

            foreach (var id in userIds)
            {
                var basket = await _repository.GetBasketAsync(Int32.Parse(id));

                await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice, basket);
            }
        }

        private async Task UpdatePriceInBasketItems(int productId, decimal newPrice, decimal oldPrice, CustomerBasket basket)
        {
            var itemsToUpdate = basket?.Items?.Where(x => x.ProductId == productId).ToList();
            if (itemsToUpdate != null)
            {
                _logger.LogInformation("----- ProductPriceChangedIntegrationEventHandler - " +
                    "Updating items in basket for user: {BuyerId} ({@Items})", basket.BuyerId, itemsToUpdate);

                foreach (var item in itemsToUpdate)
                {
                    if (item.UnitPrice == oldPrice)
                    {
                        var originalPrice = item.UnitPrice;
                        item.UnitPrice = newPrice;
                        item.OldUnitPrice = originalPrice;
                    }
                }
                await _repository.UpdateBasketAsync(basket);
            }
        }
    }
}
