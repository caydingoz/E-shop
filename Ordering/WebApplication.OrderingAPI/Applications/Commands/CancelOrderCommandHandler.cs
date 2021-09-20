using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.OrderingDomain.Models;

namespace WebApplication.OrderingAPI.Applications.Commands
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _repository;
        public CancelOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _repository.GetOrderByIdAsync(command.OrderId);
            orderToUpdate.LastModifiedDate = DateTime.Now.AddHours(3);
            var result = orderToUpdate.SetCancelledStatus();
            if (!result) return false;
            await _repository.SaveChangesDb();
            return true;
        }
    }
}
