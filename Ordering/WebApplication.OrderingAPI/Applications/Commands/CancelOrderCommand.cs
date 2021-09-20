using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.OrderingAPI.Applications.Commands
{
    public class CancelOrderCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
    }
}
