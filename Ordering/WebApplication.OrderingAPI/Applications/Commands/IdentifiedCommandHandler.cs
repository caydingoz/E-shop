using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.OrderingDomain.Models;

namespace WebApplication.OrderingAPI.Applications.Commands
{
    public class IdentifiedCommandHandler<T> : IRequestHandler<IdentifiedCommand<T>, bool> where T : IRequest<bool>
    {
        private readonly IOrderRepository _repository;
        public IdentifiedCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(IdentifiedCommand<T> command, CancellationToken cancellationToken)
        {
            bool result = false;
            switch (command.Command)
            {
                case CancelOrderCommand cancelOrderCommand:
                    var orderToUpdate = await _repository.GetOrderByIdAsync(cancelOrderCommand.OrderId);
                    if (orderToUpdate.BuyerId == command.CustomerId) result = true;
                    break;
            }
            return result;
        }
    }
}
