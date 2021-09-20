using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.OrderingDomain.Models;

namespace WebApplication.OrderingAPI.Applications.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository repository, ILogger<CreateOrderCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<bool> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = new Order(command.BuyerId, command.BuyerName, command.Adress, command.CardNumber);

            foreach (var item in command.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Quantity);
            }
            order.TotalPrice = order.GetTotal();
            _logger.LogInformation("Creating Order - Order: {@Order}", order);
            await _repository.AddOrder(order);
            return true;
        }
    }
}
