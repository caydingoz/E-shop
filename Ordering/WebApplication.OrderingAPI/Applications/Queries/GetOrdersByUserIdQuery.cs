using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.OrderingDomain.Models;

namespace WebApplication.OrderingAPI.Applications.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<List<Order>>
    {
        public GetOrdersByUserIdQuery(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; }
    }
}
