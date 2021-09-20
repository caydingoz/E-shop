using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.OrderingDomain.Models;

namespace WebApplication.OrderingAPI.Applications.Queries
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, List<Order>>
    {
        private readonly IOrderRepository _repository;
        public GetOrdersByUserIdQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Order>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetOrdersByUserIdAsync(request.UserId);
        }
    }
}
