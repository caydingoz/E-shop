using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.OrderingDomain.Models;

namespace WebApplication.OrderingAPI.Applications.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; }
    }
}
